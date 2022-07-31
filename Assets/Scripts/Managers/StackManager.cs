using UnityEngine;
using Controllers;
using Signals;
using Enums;
using Data.UnityObject;
using Data.ValueObject;
using DG.Tweening;
using Command;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        #region Self Veriables

        #region Public Veriables

        [Header("Data")]
        public StackData StackData;
        public ScoreData ScoreData;

        #endregion

        #region Serilazible Veriables

        [SerializeField]
        private StackDecreaseController stackDecreaseController;

        #endregion

        #region Private Veriables

        private StackIncreaseCommand _stackIncreaseCommand;        
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private StackScaleCommand _stackScaleCommand;
        private Transform _playerManager;
        private float _StackScore;

        #endregion

        #endregion

        private void Awake()
        {
            StackData = GetStackData();
            ScoreData = GetScoreData();
            StackData.StackList.Clear();
            stackDecreaseController = GetComponent<StackDecreaseController>();
            _stackIncreaseCommand = new StackIncreaseCommand();
            _stackLerpMovementCommand = new StackLerpMovementCommand();
            _stackScaleCommand = new StackScaleCommand();
            _playerManager = FindObjectOfType<PlayerManager>().transform;
        }

        #region Event Subscription
        private ScoreData GetScoreData() => Resources.Load<CD_Score>("Data/CD_Score").ScoreData;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onCalculateStackScore += OnCalculateStackScore;
            StackSignals.Instance.onDecreaseStack += OnStackHitTheObstacleDecrease;
            StackSignals.Instance.onDelistStack += OnStackGeneralDecrease;
            StackSignals.Instance.onInitStackIncrease += OnInitStackIncrease;
           // StackSignals.Instance.onRandomThrowCollectable += OnRandomThrowCollectable;

            CoreGameSignals.Instance.onReset += OnReset;
        }

       

        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onCalculateStackScore -= OnCalculateStackScore;
            StackSignals.Instance.onDecreaseStack -= OnStackHitTheObstacleDecrease;
            StackSignals.Instance.onDelistStack -= OnStackGeneralDecrease;
            //StackSignals.Instance.onRandomThrowCollectable -= OnRandomThrowCollectable;

            CoreGameSignals.Instance.onReset -= OnReset;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion


        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").Data;
        }

        #region Stack Increase Decrease Jobs

        #region Increase Jobs
        private void OnInitStackIncrease()
        {
            
            //OnIncreaseStack(Instantiate(StackData.CollectableObj));
        }
        public void OnIncreaseStack(GameObject _obj)
        {
            StartCoroutine(_stackScaleCommand.ScaleSizeUpAndDown(StackData.StackList, StackData.StackMaxScaleValue, StackData.StackScaleDelay, StackData.StackTaskDelay));
            if (StackData.StackList.Count == 0)
            {
                var pos = new Vector3(0, _obj.transform.position.y, 1f);
                _stackIncreaseCommand.IncreaseFunc(_obj, this.gameObject, pos, StackData.StackList);
                
            }
            else
            {
                var pos = new Vector3(0, _obj.transform.position.y, StackData.StackList[StackData.StackList.Count - 1].transform.localPosition.z +
                     + .1f);

                _stackIncreaseCommand.IncreaseFunc(_obj, this.gameObject, pos, StackData.StackList);
                
            }
        }
        #endregion

        #region Decrease Jobs
        public void OnStackHitTheObstacleDecrease(GameObject _obj)
        {
            stackDecreaseController.StackHitTheObstacleDecrease(_obj, StackData.StackList);
           
        }
        public void OnStackGeneralDecrease(GameObject _obj, Transform _targetParent)
        {
            stackDecreaseController.StackGeneralDecrease(_obj,StackData.StackList, _targetParent);
            
        }

        private void OnCalculateStackScore()
        {
            _StackScore = 0;
            for (int i = 0; i < StackData.StackList.Count; i++)
            {
               CollectableType collectableType =StackData.StackList[i].GetComponent<CollectableManager>().collectableType;
               _StackScore+=((int)collectableType+1)*10*ScoreData.IncomeValue;
            }

            ScoreSignals.Instance.onScoreChange(ScoreTypes.StackScore, _StackScore);
        }

        #endregion

        #region Random Throw
        //private void OnRandomThrowCollectable(GameObject _obj)
        //{
        //    int a = StackData.StackList.IndexOf(_obj);
        //    //stackDecreaseController.ThrowRandomObj(_obj, StackData.StackList);
        //}
        #endregion

        #endregion

        #region Calculate Money Jobs
        public void OnCalculateTotalStackMoney(int _value)
        {

        }

        #endregion

        private void FixedUpdate()
        {
            _stackLerpMovementCommand.StackLerpMovement(StackData.StackList, _playerManager, StackData.StackLerpDelay);
        }

        private void OnReset()
        {
            StackData.StackList.Clear();
            StackData.StackList.TrimExcess();
        }

    }
}
