using TMPro;
using UnityEngine;

namespace Controllers
{
    public class MoneyPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private TextMeshProUGUI moneyText;

        #endregion

        #endregion


        public void SetMoneyText(float value)
        {
            value= Mathf.Floor(value);
            moneyText.text = (value).ToString();
        }

    }
}

