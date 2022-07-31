using UnityEngine;
using DG.Tweening;
namespace Command
{
    public class ApplyShakeAnimationCommand
    {
        private Tween _tween;
        public void ApplyShakeAnimation(Transform atmTransform)
        {
            if (_tween != null)
                _tween.Kill(true);
            _tween = atmTransform.DOShakeScale(0.1f, 0.1f, 1, 0, true);
        }
    }
}