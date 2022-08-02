//Stylized Water 2
//Staggart Creations (http://staggart.xyz)
//Copyright protected under Unity Asset Store EULA

//Schlick's BRDF fresnel
float ReflectionFresnel(float3 worldNormal, float3 viewDir, float exponent)
{
	float cosTheta = saturate(dot(worldNormal, viewDir));
	return pow(max(0.0, 1.0 - cosTheta), exponent);
}

float3 SampleReflections(float3 reflectionVector, float smoothness, float4 screenPos, float3 wPos, float3 normal, float3 viewDir, float2 pixelOffset)
{
	/* Not yet available
	#if VERSION_GREATER_EQUAL(12,0)
	float3 probe = saturate(GlossyEnvironmentReflection(reflectionVector, wPos, smoothness, 1.0)).rgb;
	#else
	float3 probe = saturate(GlossyEnvironmentReflection(reflectionVector, smoothness, 1.0)).rgb;
	#endif
	*/
	
	float3 probe = saturate(GlossyEnvironmentReflection(reflectionVector, smoothness, 1.0)).rgb;

	#if !_RIVER //Planar reflections are pointless on curve surfaces, skip
	screenPos.xy += pixelOffset.xy;
	screenPos /= screenPos.w;

	float planarMask = 1-saturate(dot(viewDir, normal));
	planarMask = saturate(pow(planarMask, _PlanarReflectionsParams));
	
	float4 planarLeft = SAMPLE_TEXTURE2D(_PlanarReflectionLeft, sampler_PlanarReflectionLeft, screenPos.xy);
	
	return lerp(probe, planarLeft.rgb, planarLeft.a * planarMask * _PlanarReflectionsEnabled);
	#else
	return probe;
	#endif
}

//Reusable for every light
struct TranslucencyData
{
	float3 subsurfaceColor;
	float3 lightColor;
	float3 lightDir;
	float3 viewDir;
	float3 normal;
	float mask;
	float strength;
	float exponent;

};

TranslucencyData PopulateTranslucencyData(float3 subsurfaceColor, float3 lightDir, float3 lightColor, float3 viewDir, float3 WorldNormal, float3 worldTangentNormal, float mask, float4 params)
{
	#define STRENGTH params.x
	#define EXPONENT params.y
	
	TranslucencyData d = (TranslucencyData)0;
	//TODO: Modulate with light color
	d.subsurfaceColor = subsurfaceColor;
	d.lightColor = lightColor;
	d.lightDir = lightDir;
	d.normal = WorldNormal;
	d.mask = mask; //Shadows, foam, intersection, etc
	//Light intensity is pre-multiplied with color in URP, decode by magnitude of color "vector"
	d.strength = STRENGTH;
	d.viewDir = viewDir;
	d.exponent = EXPONENT;

	return d;
}

//Single channel overlay
float BlendOverlay(float a, float b)
{
	return (b < 0.5) ? 2.0 * a * b : 1.0 - 2.0 * (1.0 - a) * (1.0 - b);
}

//RGB overlay
float3 BlendOverlay(float3 a, float3 b)
{
	float3 color;
	color.r = BlendOverlay(a.r, b.r);
	color.g = BlendOverlay(a.g, b.g);
	color.b = BlendOverlay(a.b, b.b);
	return color;
}

#define TRANSLUCENCY_INTENSITY 1 //Far too faint when using natural values

void ApplyTranslucency(float3 subsurfaceColor, float3 lightDir, float3 lightColor, float3 viewDir, float3 normal, float mask, float4 params, inout float3 emission)
{
	#define STRENGTH params.x
	#define EXPONENT params.y
	
	//TODO: Perturb light vector by normal with controllable strength
	float VdotL = saturate(pow(saturate(dot(-viewDir, lightDir - (normal * 0.0)) - mask), EXPONENT));

	#if _ADVANCED_SHADING
	//Fade the effect out as the sun approaches the horizon (80 to 90 degrees)
	half sunAngle = saturate(dot(float3(0, 1, 0), lightDir));
	half angleMask = saturate(sunAngle * 10); /* 1.0/0.10 = 10 */
	VdotL *= angleMask;
	#endif

	//In URP light intensity is pre-multiplied with the color, extract via magnitude of color "vector"
	VdotL *= STRENGTH * length(lightColor) * TRANSLUCENCY_INTENSITY;
	
#if _ADVANCED_SHADING
	subsurfaceColor = BlendOverlay(lightColor, subsurfaceColor);
	emission += subsurfaceColor * VdotL;
#else //Simple shading
	emission = lerp(emission, subsurfaceColor, VdotL);
#endif
}

void ApplyTranslucency(TranslucencyData translucencyData, inout float3 baseColor)
{
	ApplyTranslucency(translucencyData.subsurfaceColor, translucencyData.lightDir, translucencyData.lightColor, translucencyData.viewDir, translucencyData.normal, translucencyData.mask, float4(translucencyData.strength, translucencyData.exponent, 0, 0),  baseColor);
}

//Based on UniversalFragmentBlinnPhong (no BRDF)
float3 ApplyLighting(SurfaceData surfaceData, InputData inputData, TranslucencyData translucencyData, float density, float vFace)
{
	//Hold sun reflection, mask by water density for backface
	surfaceData.specular.rgb *= lerp((1-density), 1, vFace);

	#if _TRANSLUCENCY
	ApplyTranslucency(translucencyData, surfaceData.emission.rgb);
	#endif
	
	float3 color = surfaceData.albedo.rgb + surfaceData.emission.rgb + surfaceData.specular;
	
#if !_UNLIT
	Light mainLight = GetMainLight(inputData.shadowCoord);

	//Allow shadow strength to be overridden.
	mainLight.shadowAttenuation = saturate(mainLight.shadowAttenuation + (1.0 - (_ShadowStrength * vFace)));

	MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, half4(0, 0, 0, 0));

	half3 attenuatedLightColor = mainLight.color * (mainLight.distanceAttenuation * mainLight.shadowAttenuation);
	half3 diffuseColor = inputData.bakedGI + LightingLambert(attenuatedLightColor, mainLight.direction, inputData.normalWS);	

#if _ADDITIONAL_LIGHTS //Per pixel lights
	uint pixelLightCount = GetAdditionalLightsCount();
	for (uint lightIndex = 0u; lightIndex < pixelLightCount; ++lightIndex)
	{
		Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
		half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
		diffuseColor += LightingLambert(attenuatedLightColor, light.direction, inputData.normalWS);

		#if _TRANSLUCENCY && _ADVANCED_SHADING
		TranslucencyData lightTranslucencyData = PopulateTranslucencyData(translucencyData.subsurfaceColor, light.direction, attenuatedLightColor, inputData.viewDirectionWS, inputData.normalWS, inputData.normalWS, translucencyData.mask , translucencyData.strength * 16 * (light.distanceAttenuation));
		ApplyTranslucency(lightTranslucencyData, diffuseColor);
		#endif

		//Fast blinn-phong specular
		surfaceData.specular += LightingSpecular(attenuatedLightColor, light.direction, inputData.normalWS, inputData.viewDirectionWS, half4(light.color.rgb, 0), _PointSpotLightReflectionExp) * (_PointSpotLightReflectionExp * 0.03125); //= 1.0/32
	}
#endif

#ifdef _ADDITIONAL_LIGHTS_VERTEX //Previous calculated in vertex stage
	diffuseColor += inputData.vertexLighting;
#endif

	color = diffuseColor * surfaceData.albedo + surfaceData.emission + surfaceData.specular;
#else
	
#endif

	return color;
}

//Force retrieve shadows, even when shadows are disabled for the material
float GetShadows(float3 wPos)
{
	float4 shadowCoord = TransformWorldToShadowCoord(wPos.xyz);

	ShadowSamplingData shadowSamplingData = GetMainLightShadowSamplingData();
	half4 shadowParams = GetMainLightShadowParams();
	return SampleShadowmap(TEXTURE2D_ARGS(_MainLightShadowmapTexture, sampler_MainLightShadowmapTexture), shadowCoord, shadowSamplingData, shadowParams, false);
}