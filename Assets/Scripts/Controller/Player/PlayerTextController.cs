using UnityEngine;
using TMPro;
namespace Controllers
{
    public class PlayerTextController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro playerScoreText;

        #endregion
        

        #endregion
        public void UpdatePlayerScore(float totalScore)
        {
            totalScore=Mathf.Floor(totalScore);
            playerScoreText.text = totalScore.ToString();
        }
    }
}