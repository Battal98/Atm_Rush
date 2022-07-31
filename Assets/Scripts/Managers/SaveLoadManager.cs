using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extentions;
using Signals;
using System;
using Controllers;
using Keys;


namespace Managers
{
    public class SaveLoadManager : MonoBehaviour
    {
        #region Self Veriables

        #region Prive Veriables

        private int newSaveLevelData;

        
        #endregion

        #region Serilazible Veriables

        [SerializeField] private SaveDataController _saveController;
        [SerializeField] private LoadDataController _loadController;

        #endregion

        #endregion
        private void SyncLoadDataToSaveData()
        {
            _saveController.SaveDataParams.InComeLevel = _loadController.LoadDataParams.NewInCome;
            _saveController.SaveDataParams.InComePrice = _loadController.LoadDataParams.NewInComePrice;
            _saveController.SaveDataParams.StackLevel = _loadController.LoadDataParams.NewStackLevel;
            _saveController.SaveDataParams.StackLevelPrice = _loadController.LoadDataParams.NewStackLevelPrice;
            _saveController.SaveDataParams.TotalWealth = _loadController.LoadDataParams.NewTotalWealth;
            _saveController.SaveDataParams.Level = _loadController.LoadDataParams.NewLevel;
            _saveController.SaveDataParams.InstantiateLevel = _loadController.LoadDataParams.NewInstantiateLevel;
            _saveController.SaveDataParams.CharButtonName=_loadController.LoadDataParams.NewCharButtonName;

        }


        #region Event Subscribtion
        private void OnEnable()
        {
            SubscribeEvents();

        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            CoreGameSignals.Instance.onGamePause += OnGamePause;
            CoreGameSignals.Instance.onSetLevelID += OnSetLevelID;
            CoreGameSignals.Instance.onSetInstantiateLevelID += OnSetInstantiateLevelID;
            CoreGameSignals.Instance.onResetLevel += OnResetLevel;
            //CoreGameSignals.Instance.onWhencrash�tem += OnWhencrash�tem;
            SaveLoadSignals.Instance.onSetTotalWealth += OnSetTotalWealth;
            SaveLoadSignals.Instance.onSetStackLevelAndPrice += OnSetStackLevelAndPrize;
            SaveLoadSignals.Instance.onSetInComeLevelAndPrice += OnSetInComeLevelAndPrize;


        }
        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onGamePause -= OnGamePause;
            CoreGameSignals.Instance.onSetLevelID -= OnSetLevelID;
            CoreGameSignals.Instance.onResetLevel -= OnResetLevel;
            CoreGameSignals.Instance.onSetInstantiateLevelID -= OnSetInstantiateLevelID;

            //CoreGameSignals.Instance.onWhencrash�tem -= OnWhencrash�tem;
            SaveLoadSignals.Instance.onSetTotalWealth -= OnSetTotalWealth;
            SaveLoadSignals.Instance.onSetStackLevelAndPrice -= OnSetStackLevelAndPrize;
            SaveLoadSignals.Instance.onSetInComeLevelAndPrice -= OnSetInComeLevelAndPrize;

        }

       
        private void OnDisable()
        {

            UnSubscribeEvents();
        }
        #endregion

        private void OnGameOpen()
        {
            Load();

        }


        private void OnGamePause(bool value)
        {

            if (value == false) Save();
            else Load();
        }

        private void Load()
        {   
            _loadController.LoadData();
            OnGetTotalWealth();
            OnGetStackLevelAndPrize();
            OnGetInComeLevelAndPrize();
            OngetLevelID();
            OnGetInstantiateLevelID();
            SyncLoadDataToSaveData();
        }
        private void Save()
        {
            _saveController.SaveData();
        }

        private void OnResetLevel()
        {
            _saveController.SaveDataParams.Level = 0;
        }



        #region SaveValue
        private void OnSetTotalWealth(float value)
        {
            _saveController.SaveDataParams.TotalWealth = value;
        }
        private void OnSetStackLevelAndPrize(int _levelValue, int _prizeValue)
        {
            _saveController.SaveDataParams.StackLevel = _levelValue;
            _saveController.SaveDataParams.StackLevelPrice = _prizeValue;
        }
        private void OnSetInComeLevelAndPrize(int _levelValue, int _prizeValue)
        {
            _saveController.SaveDataParams.InComeLevel = _levelValue;
            _saveController.SaveDataParams.InComePrice = _prizeValue;
        }
        private void OnSetLevelID(int value)
        {   
            
            _saveController.SaveDataParams.Level=value;
        }

        private void OnSetInstantiateLevelID(int value)
        {
            _saveController.SaveDataParams.InstantiateLevel = value;
        }
        #endregion

        #region LoadValue
        
        private void OnGetTotalWealth()
        {
            
            SaveLoadSignals.Instance.onGetTotalWealth?.Invoke(_loadController.LoadDataParams.NewTotalWealth);
            
        }

        private void OnGetInstantiateLevelID()
        {
            CoreGameSignals.Instance.onGetInstantiateLevelID?.Invoke(_loadController.LoadDataParams.NewInstantiateLevel);
        }
        private void OnGetStackLevelAndPrize()
        {
            SaveLoadSignals.Instance.onGetStackLevelAndPrize?.Invoke(_loadController.LoadDataParams.NewStackLevel, _loadController.LoadDataParams.NewStackLevelPrice);
        }
        private void OnGetInComeLevelAndPrize()
        {
            SaveLoadSignals.Instance.onGetInComeLevelAndPrize?.Invoke(_loadController.LoadDataParams.NewInCome, _loadController.LoadDataParams.NewInComePrice);
        }
        private void OngetLevelID()
        {
            CoreGameSignals.Instance.onGetLevelID?.Invoke(_loadController.LoadDataParams.NewLevel);
        } 
        #endregion














    }

}