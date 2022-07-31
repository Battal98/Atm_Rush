using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {

        public UnityAction onSetCameraTarget = delegate { };

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
