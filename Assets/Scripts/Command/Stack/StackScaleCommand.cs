using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace Command
{
    public class StackScaleCommand
    {
        Tween tween;
        public IEnumerator ScaleSizeUpAndDown(List<GameObject> _stackList, float _maxScaleValueData, float _scaleDelay, float _stackDelay)
        {
            for (int i = 0; i <= _stackList.Count-1; i++)
            {
                int index = (_stackList.Count - 1) - i;

               
                _stackList[index].transform.DOScale(_maxScaleValueData, _scaleDelay);
                _stackList[index].transform.DOScale(Vector3.one, _scaleDelay).SetDelay(_stackDelay);
                yield return new WaitForSeconds(_stackDelay);
            }
        }
    } 
}
