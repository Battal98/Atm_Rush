using UnityEngine;
using UnityEngine.Events;
using Extentions;

namespace Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate{ };
        public UnityAction<GameObject> onDecreaseStack = delegate{ };
        public UnityAction<GameObject, Transform> onDelistStack = delegate{ };
       //public UnityAction<GameObject> onRandomThrowCollectable = delegate{ };
        public UnityAction onCalculateStackScore = delegate{ };
        public UnityAction onInitStackIncrease=delegate {  };

        protected override void Awake()
        {
            base.Awake();
        }
    } 

}
