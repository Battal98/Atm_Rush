using System;
using System.Collections.Generic;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveLoadSignals:MonoSingleton<SaveLoadSignals>
    {
        public UnityAction<float> onSetTotalWealth = delegate { };
        public UnityAction<int,int> onSetStackLevelAndPrice = delegate { };
        public UnityAction<int,int> onSetInComeLevelAndPrice = delegate { };
        public UnityAction<float> onGetTotalWealth = delegate { };
        public UnityAction<int,int> onGetStackLevelAndPrize = delegate { };
        public UnityAction<int,int> onGetInComeLevelAndPrize = delegate { };
        public UnityAction onResetLevel =delegate { };
        public UnityAction<string> onSetPlayerButtonName = delegate { };


        protected override void Awake()
        {
            base.Awake();
        }

    }
}
