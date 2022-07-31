using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanels> onOpenPanel;
        public UnityAction<UIPanels> onClosePanel;
        //public UnityAction<int> onSetLevelText;
        public UnityAction<float> onSetMoneyText;
        public UnityAction<int,int> onSetStackPrizeAndLevelText;
        public UnityAction<int,int> onSetIncomePrizeAndLevelText;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}