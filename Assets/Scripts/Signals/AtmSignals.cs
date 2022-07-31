using Extentions;
using UnityEngine.Events;
using UnityEngine;

namespace Signals
{
    public class AtmSignals : MonoSingleton<AtmSignals>
    {
        public UnityAction<int,bool> onAtmScoreUpdate = delegate { };
    }
}