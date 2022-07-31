using System;
using DG.Tweening;
using UnityEngine;
namespace Command
{
    public class ApplyAtmAnimationCommand
{
    public void ApplyAtmAnimation(Transform endOfDotweenPoint,GameObject collectable)
    {
        
        collectable.transform.DOScale(0, .2f);
        collectable.transform.DOMove(endOfDotweenPoint.position, .2f);
    }
}
}