using Extentions;
using UnityEngine.Events;
using System;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onGameOpen = delegate { };
        public UnityAction onGameClose = delegate { };
        public UnityAction<bool> onGamePause = delegate { };
        public UnityAction onPlay=delegate { };
        public UnityAction onReset=delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction<int> onSetLevelID = delegate { };
        public UnityAction<int> onGetLevelID = delegate { };
        public UnityAction<int> onSetInstantiateLevelID = delegate { };
        public UnityAction<int> onGetInstantiateLevelID = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction<int> onGetMoney = delegate { };
        public UnityAction<int> onSetMoney = delegate { };
        public UnityAction onResetLevel = delegate { };

        //public UnityAction<SaveGameDataParams> onSaveGameData = delegate { };//TODO:SaveSignals acilcak
        //public UnityAction<LoadGameDataParams> onLoadGameData = delegate { };

        protected override void Awake()
        {
            base.Awake();
        }
    } 
}
