using UnityEngine;
using Signals;
using System.Collections.Generic;
using Managers;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public bool isITaken;

        #endregion

        #region Serializable Variables

        [SerializeField]
        private MeshFilter m_Mesh;

        [SerializeField]
        private CollectableManager _collectableManager;

        #endregion

        #region Private Variables


        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") )
            {
                if (!isITaken)
                {   
                    StackSignals.Instance.onIncreaseStack?.Invoke(transform.parent.gameObject);
                    isITaken = true;
                }
            }

            if (other.gameObject.CompareTag("Collectable")&& isITaken)
            {
                StackSignals.Instance.onCalculateStackScore?.Invoke();
                var _colphysics = other.GetComponent<CollectablePhysicsController>();
                if (!_colphysics)
                    return;
                if (!_colphysics.isITaken)
                {
                    
                    StackSignals.Instance.onIncreaseStack?.Invoke(other.transform.parent.gameObject);
                    _colphysics.isITaken = true;
                }
                else
                    return;
            }

            if (other.gameObject.CompareTag("Obstacle"))
            {   


                StackSignals.Instance.onDecreaseStack?.Invoke(this.transform.parent.gameObject);
                
                StackSignals.Instance.onCalculateStackScore?.Invoke();
                //_collectableManager.ApplyParticleEffect();

            }

            if (other.gameObject.CompareTag("ATM"))
            {
                
                OnDelistStack(other.gameObject.transform);
                _collectableManager.ApplyAtmAnimation(other.transform.GetChild(0).transform);
                AtmSignals.Instance.onAtmScoreUpdate?.Invoke((int)_collectableManager.collectableType,false);
                StackSignals.Instance.onCalculateStackScore?.Invoke();

            }

            if (other.gameObject.CompareTag("Portal"))
            {
                _collectableManager.UpdateCollectableMesh();
                StackSignals.Instance.onCalculateStackScore?.Invoke();
                //UpdateCollectableMesh(ref _collectableManager.collectableType, _collectableManager._collectableData.CollectableMeshList);
            }
            if (other.gameObject.CompareTag("Conveyor"))
            {
                _collectableManager.ApplyConveyorAnimation(other.transform.GetChild(0).transform);
                OnDelistStack(other.gameObject.transform);
            }
        }

       

        private void OnDelistStack(Transform _targetParent)
        {
            StackSignals.Instance.onDelistStack?.Invoke(this.transform.parent.gameObject, _targetParent);
        }

    }
}
