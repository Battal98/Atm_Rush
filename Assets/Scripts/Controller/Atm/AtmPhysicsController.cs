using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AtmPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField]
        private ATMManager atmManager;

        #endregion

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AtmColliderState(false);
                atmManager.ApplyShutDownAnimation();


            }
            if (other.CompareTag("Collectable"))
            {
                atmManager.ApplyShakeAnimation();
            }
        }

        private void AtmColliderState(bool atmState)
        {
            this.GetComponent<Collider>().enabled = atmState;
        }
    } 
}
