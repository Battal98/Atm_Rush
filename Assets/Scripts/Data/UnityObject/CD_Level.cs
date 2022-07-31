using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;


namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ATMRush/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public LevelData LevelData;
    }
}
