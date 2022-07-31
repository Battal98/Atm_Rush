using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_StackButton", menuName = "ATMRush/CD_StackButton", order = 0)]
    public class CD_StackButton : ScriptableObject
    {
        public StackButtonData StackButtonData;
    }
}
