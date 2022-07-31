using UnityEngine;
using DG.Tweening;
namespace Command
{
    public class ApplyShutDownAnimationCommand
    {
       public void ApplyShutDownAnimation(Transform atmTransform)
       {
           atmTransform.DOMoveY(-2.5f,0.5f);
       }
    }
}