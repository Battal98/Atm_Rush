using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "ATMRush/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public List<PoolData> PoolData=new List<PoolData>();
    }
}