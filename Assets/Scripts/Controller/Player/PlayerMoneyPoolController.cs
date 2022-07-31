using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerMoneyPoolController : MonoBehaviour
    {
        #region Seriazible Variables

        [SerializeField]
        private int PoolCount = 99;

        [SerializeField]
        private GameObject moneyObj;

        public List<GameObject> moneyList = new List<GameObject>(); 

        #endregion

        public void InstantiateMoneys()
        {
            for (int i = 0; i < PoolCount; i++)
            {
                if (i == 0)
                {
                    SetMoneyPositions(moneyObj, this.transform.position - new Vector3(0, 0.3f, 0), moneyList, this.transform);

                }
                else
                {
                    Vector3 _pos = moneyList[i - 1].transform.position - new Vector3(0, moneyObj.transform.localScale.y - 0.3f, 0);
                    SetMoneyPositions(moneyObj, _pos, moneyList, this.transform);

                }

            }
        }

        private void SetMoneyPositions(GameObject _creatableObj, Vector3 _objPos, List<GameObject> _moneyList, Transform _spawner)
        {
            var _lastMoney = Instantiate(_creatableObj, _objPos, Quaternion.identity, _spawner);
            _moneyList.Add(_lastMoney);
            _lastMoney.SetActive(false);
        }

        public void SetActiveToMoneysInPlayer()
        {
            for (int i = 0; i < moneyList.Count; i++)
            {
                moneyList[i].SetActive(true);
            }
        }


    }
}