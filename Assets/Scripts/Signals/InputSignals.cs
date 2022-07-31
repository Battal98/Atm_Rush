using Extentions;
using Keys;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        
        public UnityAction onEnableInput = delegate{ };
        public UnityAction onDisableInput = delegate{ };
        public UnityAction onFirstTimeTouchTaken = delegate{ };
        public UnityAction onInputTaken = delegate{ };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate{ };
        public UnityAction onInputReleased = delegate{ };

        protected override void Awake()
        {
            base.Awake();
        }
    }

   
}