using DG.Tweening;
using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private PlayerManager manager;
        [SerializeField] private new Collider collider;
        [SerializeField] private new Rigidbody rigidbody;
        [SerializeField] private GameObject playerObj;
        [SerializeField] private GameObject collectedObjHolder;
        [SerializeField] private GameObject handCar;

        #endregion

        #region Private Variables


        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Finish"))
            {
                MinigameArea(other.transform);
                UISignals.Instance.onOpenPanel?.Invoke(UIPanels.MiniGamePanel);
            }

            if (other.CompareTag("Obstacle"))
            {
                InputSignals.Instance.onInputReleased?.Invoke();
                playerObj.transform.DOMoveZ(transform.position.z - 15f,0.5f).OnComplete(() => InputSignals.Instance.onInputTaken?.Invoke());
            }

        }

        private void MinigameArea(Transform _target)
        {
            if (manager.CurrentScore>0) // score > 0
            {
                StartCoroutine(manager.StopPlayerAndMoveToMiniGame(_target, handCar));
            }
            else
            {
                manager.PlayerSetPositionToInfrontOfConveyor(_target);
                CoreGameSignals.Instance.onLevelFailed?.Invoke();
            }
        }
    }
}