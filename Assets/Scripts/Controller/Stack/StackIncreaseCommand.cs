using System.Collections.Generic;
using UnityEngine;


namespace Command
{
    public class StackIncreaseCommand
    {
        #region Increase Jobs
        public void IncreaseFunc(GameObject _obj ,GameObject _targetObj, Vector3 _pos, List<GameObject> _stackList)
        {
            _obj.transform.parent = _targetObj.transform;
            _obj.transform.localPosition = _pos;
            _stackList.Add(_obj);
        }
        
        #endregion
    }

}