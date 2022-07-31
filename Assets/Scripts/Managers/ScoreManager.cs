using System;
using UnityEngine;
using Signals;
using Data.ValueObject;
using Data.UnityObject;
using Enums;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public ScoreData ScoreData;


        #endregion

        #region Serialized Variables



        #endregion

        #region Private Variables



        #endregion


        #endregion

        private void Awake()
        {
            ScoreData = GetScoreData();
        }
        
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize+=OnLevelInitialize;
            ScoreSignals.Instance.onScoreChange+= OnScoreChange;
            SaveLoadSignals.Instance.onGetTotalWealth += OnGetTotalWealth;
            SaveLoadSignals.Instance.onGetInComeLevelAndPrize += OnGetIncomeLevel;
            ScoreSignals.Instance.onScoreNeedToSave += OnScoreNeedToSave;
        }

        private void OnGetIncomeLevel(int incomeLevel, int arg1)
        {
            ScoreData.IncomeValue = incomeLevel * ScoreData.IncomeMultiplier+1;
        }

        private void OnLevelInitialize()
        {
            CalculateInitScore();
        }

        private void CalculateInitScore()
        {
            StackSignals.Instance.onCalculateStackScore?.Invoke();
        }

        private void OnGetTotalWealth(float loadedScore)
        {
            ScoreData.TotalGameScore=loadedScore;
            UISignals.Instance.onSetMoneyText( ScoreData.TotalGameScore);
        }


        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onScoreChange-= OnScoreChange;
            ScoreSignals.Instance.onScoreNeedToSave -= OnScoreNeedToSave;
            SaveLoadSignals.Instance.onGetTotalWealth -= OnGetTotalWealth;
        }

        #endregion

        private ScoreData GetScoreData() => Resources.Load<CD_Score>("Data/CD_Score").ScoreData;

        #region Subscribed Methods

        private void OnScoreChange(ScoreTypes scoreTypes,float scorePoint)
        {
            CheckScoreType(scoreTypes,scorePoint);
            CalculateTotalScore();
        }

        private void CheckScoreType(ScoreTypes scoreTypes,float scorePoint)
        {
            switch(scoreTypes)
            {
                case ScoreTypes.AtmScore:
                    ScoreData.AtmScore=scorePoint;
                    break;
                case ScoreTypes.StackScore:
                    ScoreData.StackScore=scorePoint;
                    break;
                case ScoreTypes.TotalGameScore:
                    ScoreData.TotalGameScore=scorePoint;
                    if (ScoreData.TotalGameScore <= 0)
                    {
                        ScoreData.TotalGameScore = 0.1f;
                    }
                    break;
                    
            }
        }

        private void CalculateTotalScore()
        {
            ScoreData.TotalLevelScore=ScoreData.AtmScore + ScoreData.StackScore;
            //AddIncomeScore();
            PlayerSignals.Instance.onUpdateScore?.Invoke(ScoreData.TotalLevelScore);
            
            
        }

        private void AddIncomeScore()
        {
            ScoreData.TotalLevelScore*= ScoreData.IncomeValue;
        }

        private void SendTotalScoreToManagers()
        {
            SaveLoadSignals.Instance.onSetTotalWealth?.Invoke( ScoreData.TotalGameScore);
            UISignals.Instance.onSetMoneyText( ScoreData.TotalGameScore);
        }
        
        private void OnScoreNeedToSave()
        {
            ScoreData.TotalGameScore+=ScoreData.TotalLevelScore;
            SendTotalScoreToManagers();
            
            
        }

        #endregion
    }
}