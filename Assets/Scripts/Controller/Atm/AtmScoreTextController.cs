using TMPro;
using UnityEngine;

namespace Controllers
{
    public class AtmScoreTextController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
        [SerializeField] 
        private TextMeshPro atmScoreText;


        #endregion

        #region Private Variables

       
        

        #endregion


        #endregion

        public void CalculateandUpdateAtmScoreText(int value, bool _amIReset,ref float _currentScore,float _incomeValue)
        {
            if (!_amIReset)
            {
                _currentScore +=((value + 1) * 10 * _incomeValue);
                _currentScore=Mathf.Floor(_currentScore);

                atmScoreText.text = _currentScore.ToString();

            }
            else if(_amIReset)
            {
                _currentScore = value;
                atmScoreText.text = _currentScore.ToString();
            }

            
        }
    }
}