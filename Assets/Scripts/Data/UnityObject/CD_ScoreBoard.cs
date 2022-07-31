using UnityEngine;
using Data.UnityObject;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_ScoreBoard", menuName = "ATMRush/CD_ScoreBoard", order = 0)]
    public class CD_ScoreBoard : ScriptableObject
    {
        public ScoreBoardData ScoreboardData;
    }
}
