using Extentions;
using UnityEngine.Events;
using UnityEngine;

namespace Signals
{
    public class PlayerSignals:MonoSingleton<PlayerSignals>
    {
        public UnityAction<float> onUpdateScore = delegate { };
    }
}