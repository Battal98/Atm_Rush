using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ParticlePoolSignals : MonoSingleton<ParticlePoolSignals>
    {
        public UnityAction<int,Transform> onEffectNeed = delegate { };
    }
}