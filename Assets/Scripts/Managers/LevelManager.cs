using UnityEngine;
using Command;
using Signals;
using Data.UnityObject;
using Data.ValueObject;
using System.Collections.Generic;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Veriables

        #region Private Veraibles

        private int _levelID;
        private int instantiateLevelID;
        private GameObject[] _objects;

        #endregion

        #region Public Variables
        
        [Header("Data")]
        public LevelData Data;

        public List<GameObject> Levels;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject levelHolder;
        [SerializeField] private LevelLoaderCommand levelLoader;
        [SerializeField] private ClearActiveLevelCommand ClearActiveLevelCommand;

        #endregion


        #endregion

        private void GetLevelPrefabs()
        {
           _objects = Resources.LoadAll<GameObject>("Prefabs/Level");
            for (int i = 0; i < _objects.Length; i++)
            {
                Levels.Add(_objects[i]);
            }
        }

        private void Awake()
        {
            GetLevelPrefabs();
        }

        private void Start()
        {
            if (instantiateLevelID >= Levels.Count)
                instantiateLevelID = 0;
            CoreGameSignals.Instance.onLevelInitialize.Invoke();
            OnSetLevelID();
            OnSetInstantiateLevelID();
        }


        private void OnEnable()
        {
            
            SubscribeEvents();
            
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onLevelInitialize += OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onGetInstantiateLevelID += OnGetInstantiateLevelID;
            
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onLevelInitialize -= OnLevelInitialize;
            CoreGameSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            CoreGameSignals.Instance.onGetLevelID += OnGetLevelID;
            CoreGameSignals.Instance.onGetInstantiateLevelID -= OnGetInstantiateLevelID;

        }

        
        private void OnDisable()
        {
            
            UnsubscribeEvents();
            
        }

        
        private void OnLevelInitialize()
        {
           
            levelLoader.LoadLevel(levelHolder.transform, instantiateLevelID);
        }

        private void OnNextLevel()
        {

            instantiateLevelID++;
            _levelID++;
            if (instantiateLevelID >= Levels.Count)
                instantiateLevelID = 0;
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onReset?.Invoke();
            OnSetLevelID();
            OnSetInstantiateLevelID();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            CoreGameSignals.Instance.onGetLevelID?.Invoke(_levelID);
            
        }

        private void OnClearActiveLevel()
        {
            ClearActiveLevelCommand.LevelClearer(levelHolder.transform);
        }
         
        private void OnSetLevelID()
        {
            
            CoreGameSignals.Instance.onSetLevelID?.Invoke(_levelID);
        }

        private void OnSetInstantiateLevelID()
        {
            CoreGameSignals.Instance.onSetInstantiateLevelID?.Invoke(instantiateLevelID);
        }

        private void OnGetLevelID(int _levelIDValue)
        {

            _levelID =_levelIDValue;
        }

        private void OnGetInstantiateLevelID(int _instantiateLevelID)
        {

            instantiateLevelID = _instantiateLevelID;
        }


    }
}
