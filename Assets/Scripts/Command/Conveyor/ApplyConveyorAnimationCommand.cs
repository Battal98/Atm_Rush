using UnityEngine;
using DG.Tweening;
namespace Command
{
    public class ApplyConveyorAnimationCommand
    {
        public void ApplyConveyorAnimation(Transform endOfDotweenPoint,GameObject collectable)
        {
            collectable.transform.DORotate(new Vector3(0,-90,0), .2f);
            collectable.transform.DOMoveX(endOfDotweenPoint.position.x, .5f);
        }
    }
}