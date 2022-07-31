using Cinemachine;
using Signals;
using UnityEngine;
using Controllers;
using Data.ValueObject;
using Data.UnityObject;
using DG.Tweening;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables


        #region Serialized Variables

        [SerializeField]
        private CameraData data;

        [SerializeField]
        private CameraTranformController cameraTranformController;

        #endregion

        #region Private Variables

        private CinemachineVirtualCamera virtualCamera;
        private CinemachineFramingTransposer _cMFramingTransposer;

        private Vector3 _initialPosition;
        private Vector3 _initialRotation;
        private Vector3[] _camPath = new Vector3[2];

        private float _initialCamDistance;

        #endregion

        #endregion
        private void Awake()
        {
            data = GetCameraData();
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _cMFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            GetInitialPosition();
            cameraTranformController.SetCamPathRoad(_camPath, data.camPathTargets);
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {

            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
            _initialRotation = transform.eulerAngles;
            _initialCamDistance = _cMFramingTransposer.m_CameraDistance;
        }

        private void OnPlay()
        {
            cameraTranformController.SetCameraTarget(virtualCamera,_cMFramingTransposer, data._camYPosition);
            cameraTranformController.CameraMoveToPlayPosAndRot(_camPath,data.cameraOnPlayRot);
        }

        private void OnReset()
        {
            DOTween.Kill(cameraTranformController.transform);
            cameraTranformController.SetCameraMoveToInitialPosition(virtualCamera,_initialPosition,_initialRotation, _cMFramingTransposer, _initialCamDistance);
        }

        private void OnLevelSuccessful()
        {
            cameraTranformController.SetCameraMoveToFinalPos(this.gameObject,-2f, _cMFramingTransposer);

        }

        private CameraData GetCameraData()
        {
            return Resources.Load<CD_Camera>("Data/CD_Camera").CameraData;
        }
    }
}