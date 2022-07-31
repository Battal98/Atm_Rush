using TMPro;
using UnityEngine;

namespace Controllers
{
    public class LevelPanelController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] 
        private TextMeshProUGUI levelText;

        #endregion

        #endregion


        public void SetLevelText(int value)
        {
            levelText.text ="LEVEL " + (value + 1).ToString();
        }

    }
}