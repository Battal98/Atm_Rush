using System;
using Controllers;
using Enums;
using Signals;
using UnityEngine;
using Data.ValueObject;
using Data.UnityObject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {

        #region Self Variables

        #region Public Variables
        [Header("Datas")]
        public StackButtonData StackButtonData;
        public IncomeButtonData IncomeButtonData;
        public ScoreData ScoreData;
        #endregion

        #region Serialized Variables
        [Space]
        [SerializeField] 
        private UIPanelController uiPanelController;

        [SerializeField] 
        private LevelPanelController levelPanelController;

        [SerializeField]
        private MoneyPanelController moneyPanelController;

        [SerializeField]
        private StackButtonController stackButtonController;   
        
        [SerializeField]
        private IncomeButtonController incomeButtonController;

        #endregion

        #region Private Variables

        private int _levelCount;

        #endregion

        #endregion

        #region Save Data and Load Data Jobs
        /*
        private int GetStackButtonLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("StackLevel") ? ES3.Load<int> ("StackLevel") : 0;
        }
        private StackButtonData GetStackButtonData()
        {
            var newStackButtonLevelData = _stackButtonLevel % Resources.Load<CD_StackButton>("Data/CD_StackButton").StackButtonData.StackButtonLevelCount;
            return Resources.Load<CD_StackButton>("Data/CD_StackButton").StackButtonData;
        }
*/
        private void Awake()
        {
            ScoreData= GetScoreData();
        }

        private ScoreData GetScoreData() => Resources.Load<CD_Score>("Data/CD_Score").ScoreData;
        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            UISignals.Instance.onSetMoneyText += OnSetMoneyText;
            UISignals.Instance.onSetStackPrizeAndLevelText += OnGetStackButtonText;
            UISignals.Instance.onSetIncomePrizeAndLevelText += OnGetIncomeButtonText;

            #region CoreGameSignals Subscribetion

            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onGetLevelID += OnSetLevelText;
            // CoreGameSignals Came here with onplay, onlevelfailed, onlevelSuccess

            #endregion

            #region SaveLoadSignals Subscribetion

            SaveLoadSignals.Instance.onGetStackLevelAndPrize += OnGetStackButtonText;
            SaveLoadSignals.Instance.onGetInComeLevelAndPrize += OnGetIncomeButtonText;

            #endregion


        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            UISignals.Instance.onSetMoneyText -= OnSetMoneyText;
            UISignals.Instance.onSetStackPrizeAndLevelText -= OnGetStackButtonText;
            UISignals.Instance.onSetIncomePrizeAndLevelText -= OnGetIncomeButtonText;

            #region CoreGameSignals Unsubscribetion

            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onGetLevelID -= OnSetLevelText;
            
            // CoreGameSignals Came here with onplay, onlevelfailed, onlevelSuccess

            #endregion

            #region SaveLoadSignals Unsubscribetion

            SaveLoadSignals.Instance.onGetStackLevelAndPrize -= OnGetStackButtonText;
            SaveLoadSignals.Instance.onGetInComeLevelAndPrize -= OnGetIncomeButtonText;

            #endregion

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void OnOpenPanel(UIPanels panelParam)
        {
            uiPanelController.OpenPanel(panelParam);
        }

        private void OnClosePanel(UIPanels panelParam)
        {
            uiPanelController.ClosePanel(panelParam);
        }

        #region Set Text Jobs
        private void OnSetLevelText(int value)
        {
            levelPanelController.SetLevelText(value);
        }

        private void OnSetMoneyText(float value)
        {
            moneyPanelController.SetMoneyText(value);
        }

        private void OnGetStackButtonText(int _levelValue, int _prizeValue)
        {
            stackButtonController.SetStackPrizeAndLevelText(_levelValue, _prizeValue);
            StackButtonData.StackButtonPrize = _prizeValue;
            StackButtonData.StackButtonLevelCount = _levelValue;
        }
        private void OnGetIncomeButtonText(int _levelValue, int _prizeValue)
        {
            incomeButtonController.SetIncomePrizeAndLevelText(_levelValue, _prizeValue);
            IncomeButtonData.IncomeButtonPrize = _prizeValue;
            IncomeButtonData.IncomeButtonLevelCount = _levelValue;

        }

        #endregion

        #region Booster (Stack and Income) Jobs
        private void SetStackPrizeandLevelIndexIncrease()
        {
            if (StackButtonData.StackButtonPrize <= ScoreData.TotalGameScore)
            {
                StackButtonData.StackButtonLevelCount++;
                StackButtonData.StackButtonPrize += 100;
                StackSignals.Instance.onInitStackIncrease();
            }
        }

        private void SetIncomePrizeAndLevelText()
        {
            if (IncomeButtonData.IncomeButtonPrize <= ScoreData.TotalGameScore)
            {
                IncomeButtonData.IncomeButtonLevelCount++;
                ScoreData.IncomeValue += ScoreData.IncomeMultiplier;
                ScoreData.TotalGameScore-=IncomeButtonData.IncomeButtonPrize;
                IncomeButtonData.IncomeButtonPrize += 100;
                ScoreSignals.Instance.onScoreChange?.Invoke(ScoreTypes.TotalGameScore,(int)ScoreData.TotalGameScore);
                //UISignals.Instance.onSetMoneyText?.Invoke(ScoreData.TotalGameScore);
                ScoreSignals.Instance.onScoreNeedToSave?.Invoke();
            }
        }

        public void ClickIncomeButton()
        {
            //stackSignals invoke 
            SetIncomePrizeAndLevelText();
            UISignals.Instance.onSetIncomePrizeAndLevelText?.Invoke(IncomeButtonData.IncomeButtonLevelCount, IncomeButtonData.IncomeButtonPrize);
            SaveLoadSignals.Instance.onSetInComeLevelAndPrice?.Invoke(IncomeButtonData.IncomeButtonLevelCount, IncomeButtonData.IncomeButtonPrize);
        }

        public void ClickStackButton()
        {
            SetStackPrizeandLevelIndexIncrease();
            UISignals.Instance.onSetStackPrizeAndLevelText?.Invoke(StackButtonData.StackButtonLevelCount, StackButtonData.StackButtonPrize);
            SaveLoadSignals.Instance.onSetStackLevelAndPrice?.Invoke(StackButtonData.StackButtonLevelCount, StackButtonData.StackButtonPrize);
        }

        #endregion

        #region Click Store Props
        public void OnClickStoreButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StorePanel);
        }

        #endregion

        private void OnPlay()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.LevelPanel);
        }

        private void OnLevelFailed()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);

            //CoreGameSignals.Instance.onLevelFailed?.Invoke();
        }

        private void OnLevelSuccessful()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.WinPanel);

            //CoreGameSignals.Instance.onLevelSuccessful?.Invoke();// Trigger in Final 
        }

        public void Play()
        {
            //CoreGameSignals onplay Invoke here
            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void NextLevel()
        {
            //CoreGameSignals onnextlevel Invoke here
            CoreGameSignals.Instance.onNextLevel?.Invoke();

            UISignals.Instance.onClosePanel?.Invoke(UIPanels.WinPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
        }

        public void RestartLevel()
        {
            //CoreGameSignals onrestart Invoke here
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.FailPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void ReTryLevel()
        {
            UISignals.Instance.onClosePanel?.Invoke(UIPanels.LevelPanel);
            UISignals.Instance.onOpenPanel?.Invoke(UIPanels.StartPanel);
            CoreGameSignals.Instance.onReset?.Invoke();
        }

        public void OnClickCloseButton(GameObject _closeButtonObj)
        {
            UISignals.Instance.onOpenPanel.Invoke(UIPanels.StartPanel);
            _closeButtonObj.transform.parent.gameObject.SetActive(false);
        }

        public void ResetLevelCount()
        {
            UISignals.Instance.onOpenPanel.Invoke(UIPanels.StartPanel);
            SaveLoadSignals.Instance.onResetLevel?.Invoke();
        }
    }
}