using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public class StackLerpMovementCommand
    {
        #region Stack Lerp Movement Jobs
        public void StackLerpMovement(List<GameObject> _stackList, Transform _playerManager, float _stackLerpDelay)
        {
            if (_stackList.Count > 0)
            {
                _stackList[0].transform.position = _playerManager.position + new Vector3(0, 0.65f, _playerManager.localScale.z * 1.7f);
                for (int i = 1; i < _stackList.Count; i++)
                {
                    _stackList[i].transform.position = Vector3.Lerp(_stackList[i].transform.position,
                        new Vector3(_stackList[i - 1].transform.position.x, 0.65f, _stackList[i - 1].transform.position.z + _playerManager.localScale.z * 1.4f),
                        _stackLerpDelay);
                }
            }
        }
        #endregion
    }

}