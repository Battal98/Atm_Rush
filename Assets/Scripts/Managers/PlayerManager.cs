using System;
using Controllers;
using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using Enums;
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region PublicVariables
        
        public float CurrentScore;

        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerTextController textController;
        [SerializeField] private PlayerMoneyPoolController playerMoneyPoolController;
        [SerializeField] private PlayerMinigamePhysicController playerMinigamePhysicController;
        [SerializeField] private PlayerMeshController playerMeshController;



        #endregion

        #region PrivateVariables

        private PlayerData Data;
        private float totalHeightScore;

        #endregion

        #endregion

        #region EventSubscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            PlayerSignals.Instance.onUpdateScore += OnUpdateScore;
            InputSignals.Instance.onInputReleased += OnDeactivateMovement;
            InputSignals.Instance.onInputDragged += OnGetInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
            ShopSignal.Instance.onChangeChar += OnChangeChar;
        }


        private void OnUpdateScore(float totalScore)
        {
            textController.UpdatePlayerScore(totalScore);
            CurrentScore = totalScore;
        }

        private void UnSubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            PlayerSignals.Instance.onUpdateScore -= OnUpdateScore;
            InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
            InputSignals.Instance.onInputDragged -= OnGetInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
            CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
            ShopSignal.Instance.onChangeChar -= OnChangeChar;
        }

        #endregion

        private void OnChangeChar(string withButtonName)
        {
            playerMeshController.ChangePlayer(withButtonName);

        }

        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }

        private void Start()
        {
            playerMoneyPoolController.InstantiateMoneys();
        }

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.movementData);
        }

        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").Data;

        #region SubscribedMethods

        private void OnLevelSuccessful()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnLevelFailed()
        {
            movementController.IsReadyToPlay(false);
        }

        private void OnPlay()
        {
            animationController.ChangeAnimationState(PlayerAnimationStates.Walk);
            movementController.IsReadyToPlay(true);
        }

        private void OnDeactivateMovement()
        {
            movementController.DisableMovement();
        }

        private void OnGetInputValues(HorizontalInputParams inputParam)
        {
            movementController.UpdateInputValue(inputParam);
        }

        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnReset()
        {
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke();
            OnUpdateScore(0);

        }

        #endregion

        #region Player Mini Game Movement and Finish Jobs

        private void DisablePlayerTouch()
        {
            InputSignals.Instance.onDisableInput?.Invoke();
        }
        private void StopPlayer()
        {
            DisablePlayerTouch();
            movementController.IsReadyToPlay(false);
            var _col = GetComponentInChildren<Collider>();
            _col.enabled = false;
        }
        public void PlayerSetPositionToInfrontOfConveyor(Transform _playerTarget)
        {
            StopPlayer();
            this.transform.DOMove(_playerTarget.position + new Vector3(0, 0, -5f), 0.1f);
        }
        public IEnumerator StopPlayerAndMoveToMiniGame(Transform _playerTarget, GameObject _handCar)
        {
            PlayerSetPositionToInfrontOfConveyor(_playerTarget);
            yield return new WaitForSeconds(1f);
            PlayerMoveToInFrontofCubes(_playerTarget, _handCar);
        }

        private void PlayerMoveToInFrontofCubes(Transform _playerTarget, GameObject _handCar)
        {
            _handCar.SetActive(false);
            var _getTarget = _playerTarget.transform.GetChild(0);
            this.transform.DORotate(new Vector3(0, 180, 0), 1f);
            this.transform.DOMove(_getTarget.gameObject.transform.position, 1f).OnComplete(() =>
            {
                animationController.ChangeAnimationState(PlayerAnimationStates.Dance); // Dans Animation 
                playerMoneyPoolController.SetActiveToMoneysInPlayer();
                CalculatePlayerHeightCanReach();
                PlayerMoveToUp();

            });
        }
        private void CalculatePlayerHeightCanReach() // split
        {
            totalHeightScore = CurrentScore / 11 * 4;
            if (totalHeightScore >= 365)
            {
                totalHeightScore = 365;
            }
        }
        private void PlayerMoveToUp()
        {
            this.transform.DOMoveY(totalHeightScore, 3).SetEase(Ease.InOutExpo).OnComplete(() =>
            {

                var totalMiniGameScore = playerMinigamePhysicController.GetMultiplyValueAndCalculated(CurrentScore);
                PlayerIsSuccessSignalInvokes(totalMiniGameScore);

            });
        }
        private void PlayerIsSuccessSignalInvokes(float _totalMinigameScore)
        {
            ScoreSignals.Instance.onScoreChange?.Invoke(ScoreTypes.AtmScore, _totalMinigameScore);
            ScoreSignals.Instance.onScoreNeedToSave?.Invoke();
            CoreGameSignals.Instance.onLevelSuccessful?.Invoke();
        }

        #endregion



    }
}