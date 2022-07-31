using UnityEngine;
using Controllers;
using Command;
using Data.ValueObject;
using Data.UnityObject;
using Signals;


namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        public CollectableData _collectableData;
        

        #endregion
        #region Serializable Variables

        [SerializeField]
        public CollectableType collectableType;

        [SerializeField]
        private CollectablePhysicsController collectablePhysicsController;

        [SerializeField]
        private CollectableMeshController collectableMeshController;
        [SerializeField]
       
        #endregion

        #region Private Variables
        
        private ApplyAtmAnimationCommand ApplyAtmAnimationCommand;
        private ApplyConveyorAnimationCommand ApplyConveyorAnimationCommand;

        #endregion

        #endregion

        private void Awake()
        {
            ApplyAtmAnimationCommand=new ApplyAtmAnimationCommand();
            ApplyConveyorAnimationCommand=new ApplyConveyorAnimationCommand();
            _collectableData = GetCollectableData();
            collectableType = new CollectableType();
        }
        private CollectableData GetCollectableData()
        {
            return Resources.Load<CD_Collectable>("Data/CD_Collectable").CollectableData;
        }

       
        public void ApplyAtmAnimation(Transform endOfDotweenPoint)
        {
            
            ApplyAtmAnimationCommand.ApplyAtmAnimation(endOfDotweenPoint,this.gameObject);
        }public void ApplyConveyorAnimation(Transform endOfDotweenPoint)
        {
            ApplyConveyorAnimationCommand.ApplyConveyorAnimation(endOfDotweenPoint,this.gameObject);
        }

        public void UpdateCollectableMesh()
        {
            collectableMeshController.UpdateCollectableMesh(ref collectableType,_collectableData.CollectableMeshList);
        }

        public void ApplyCollectableParticleEffect()
        {
            ParticlePoolSignals.Instance.onEffectNeed?.Invoke((int)collectableType,transform);
        }
    }
}
