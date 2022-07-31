using UnityEngine;
using TMPro;


namespace Controllers
{
    public class IncomeButtonController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private TextMeshProUGUI incomeLevelText;
        [SerializeField]
        private TextMeshProUGUI incomePrizeText;

        #endregion

        #endregion


        public void SetIncomePrizeAndLevelText(int _levelValue, int _prizeValue )
        {
            incomePrizeText.text = (_prizeValue).ToString() + "$";
            incomeLevelText.text = "Level " + (_levelValue).ToString();
        }
    }
}
