using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Controllers
{
    public class MinigameController : MonoBehaviour
    {
        private float _minValue = 10f, _maxValue=100f;
        public void SetBoardPositionsAndTexts(GameObject _creatableObj, Vector3 _objPos, List<GameObject> _cubeList, Transform _spawner, float _multiplyValue)
        {
            var _boardCubes = Instantiate(_creatableObj, _objPos, Quaternion.identity, _spawner);
            SetBoardText(_boardCubes, _multiplyValue);
            _cubeList.Add(_boardCubes);
        }

        public void SetBoardText(GameObject _createdObj, float _multiplyValue)
        {
            var _text = _createdObj.GetComponentInChildren<TextMeshPro>();
            if (_minValue > _maxValue)
                return;
            float _value = _minValue / _multiplyValue;
            _minValue++;
            _text.text = _value.ToString() + "X";
        }

    }
}
