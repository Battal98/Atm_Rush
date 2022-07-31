using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PoolData
    {
        public int PoolAmount =3 ;
        public GameObject ObjectType;
        public List<GameObject> PoolObjects=new List<GameObject>();
        
    }
}