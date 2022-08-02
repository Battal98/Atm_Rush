using System.Collections.Generic;
using UnityEngine;
using Managers;
using DG.Tweening;
using Cinemachine;

namespace Controllers
{

    public class CameraTranformController : MonoBehaviour
    {
        public void CameraMoveToPlayPosAndRot(Vector3[] _camPath, Vector3 _rotEndValue )
        {
            //SetCameraTarget();
            this.transform.DOLocalPath(_camPath, 0.5f, PathType.CatmullRom);
            this.transform.DORotate(_rotEndValue, 1f);
        }

        //command olabilir
        public void SetCameraTarget(CinemachineVirtualCamera virtualCamera, CinemachineFramingTransposer _cMFramingTransposer, float _camYPosition)
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            virtualCamera.Follow = playerManager;
            DOTween.To(() => _cMFramingTransposer.m_TrackedObjectOffset.y, y => _cMFramingTransposer.m_TrackedObjectOffset.y = y, _camYPosition, 1f);
            DOTween.To(() => _cMFramingTransposer.m_TrackedObjectOffset.x, t => _cMFramingTransposer.m_TrackedObjectOffset.x = t, 0, 1f);
            DOTween.To(() => _cMFramingTransposer.m_CameraDistance, x => _cMFramingTransposer.m_CameraDistance = x, 11, 1f);
        }

        public void SetCamPathRoad(Vector3[] _camPath, List<Transform> camPathTargets)
        {
            for (int i = 0; i < camPathTargets.Count; i++)
            {
                _camPath[i] = camPathTargets[i].localPosition;
            }
        }
        public void SetCameraMoveToInitialPosition(CinemachineVirtualCamera virtualCamera, Vector3 _initialPosition, Vector3 _initialRotation, CinemachineFramingTransposer _cMFramingTransposer, float _initialCamDistance)
        {

            virtualCamera.LookAt = null;
            transform.localPosition = _initialPosition;
            transform.eulerAngles = _initialRotation;
            DOTween.To(() => _cMFramingTransposer.m_TrackedObjectOffset.x, y => _cMFramingTransposer.m_TrackedObjectOffset.x = y, 0, 1f);
            _cMFramingTransposer.m_CameraDistance = _initialCamDistance;
            //DOTween.KillAll();
        }

        public void SetCameraMoveToFinalPos( GameObject _obj ,float _newCamPosOffset ,CinemachineFramingTransposer _cMFramingTransposer) 
        {
            _obj.transform.DORotate(new Vector3(19,-20,0),0.5f);
            DOTween.To(() => _cMFramingTransposer.m_TrackedObjectOffset.x, z => _cMFramingTransposer.m_TrackedObjectOffset.x = z, _newCamPosOffset, 0.5f);
            DOTween.To(() => _cMFramingTransposer.m_TrackedObjectOffset.y, t => _cMFramingTransposer.m_TrackedObjectOffset.y = t, 2f, 0.5f);
            DOTween.To(() => _cMFramingTransposer.m_CameraDistance, x => _cMFramingTransposer.m_CameraDistance = x, 12, 1f);
        }
    } 
}
