/* Configuration: UnityFog */

//Authors of third-party fog solutions can reach out to have their method integrated here

/* start UnityFog */
#define UnityFog
/* end UnityFog */

/* start Colorful */
//#define Colorful
/* end Colorful */

/* start Enviro */
//#define Enviro
/* end Enviro */

/* start Azure */
//#define Azure
/* end Azure */

/* start AtmosphericHeightFog */
//#define AtmosphericHeightFog
/* end AtmosphericHeightFog */

/* start SCPE */
//#define SCPE
/* end SCPE */

#ifdef Colorful
#include "Assets/ColorfulSky/Shaders/Libraries/Fog.hlsl"
#endif

#ifdef Enviro
#include "Assets/Enviro - Sky and Weather/Core/Resources/Shaders/Core/EnviroFogCore.hlsl"
#endif

#ifdef Azure
#include "Assets/Azure[Sky] Dynamic Skybox/Shaders/Transparent/AzureFogCore.cginc"
#endif

#ifdef AtmosphericHeightFog
#include "Assets/BOXOPHOBIC/Atmospheric Height Fog/Core/Includes/AtmosphericHeightFog.cginc"
#endif

#ifdef SCPE
#define DECLARE_TEX(textureName) TEXTURE2D(textureName);
#define DECLARE_RT(textureName) TEXTURE2D_X(textureName);
SAMPLER(sampler_LinearClamp);
SAMPLER(sampler_LinearRepeat);
#define Clamp sampler_LinearClamp
#define Repeat sampler_LinearRepeat
#define SAMPLE_TEX(textureName, samplerName, uv) SAMPLE_TEXTURE2D_LOD(textureName, samplerName, uv, 0)
#define SAMPLE_RT_LOD(textureName, samplerName, uv, mip) SAMPLE_TEXTURE2D_X_LOD(textureName, samplerName, uv, mip)
#include "Assets/SC Post Effects/Runtime/Fog/Fog.hlsl"
#endif

//Executed in vertex stage
float CalculateFogFactor(float3 positionCS) {
	return ComputeFogFactor(positionCS.z);
}

//Fragment stage. Note: Screen position passed here is not normalized (divided by w-component)
void ApplyFog(inout float3 color, float fogFactor, float4 screenPos, float3 wPos, float vFace) 
{
	float3 foggedColor = 0;
	
#ifdef UnityFog
	foggedColor = MixFog(color.rgb, fogFactor);
#endif

#ifdef Colorful
	foggedColor.rgb = ApplyFog(color.rgb, fogFactor, wPos, screenPos.xy / screenPos.w);
#endif
	
#ifdef Enviro
	foggedColor.rgb = TransparentFog(float4(color.rgb, 1.0), wPos, screenPos.xy / screenPos.w, fogFactor).rgb;
#endif
	
#ifdef Azure
	foggedColor.rgb = ApplyAzureFog(float4(color.rgb, 1.0), wPos).rgb;
#endif

#ifdef AtmosphericHeightFog
	float4 fogParams = GetAtmosphericHeightFog(wPos.xyz);
	foggedColor.rgb = ApplyAtmosphericHeightFog(color.rgb, fogParams);
#endif

#ifdef SCPE
	float4 dummy;
	screenPos.xy /= screenPos.w;
	ApplyFog_float(wPos, float3(0,1,0), screenPos, _TimeParameters.x, 0, color.rgb, dummy, foggedColor.rgb);
#endif

	//Fog only applies to the front faces, otherwise affects underwater rendering
	color = lerp(color, foggedColor, vFace);

}
