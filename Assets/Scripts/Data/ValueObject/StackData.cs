using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class StackData
    {
        public GameObject CollectableObj;
        public List<GameObject> StackList = new List<GameObject>();
        public float StackTaskDelay = 0.03f;
        public float StackMaxScaleValue = 2f; 
        public float StackScaleDelay = 0.25f;
        public float StackLerpDelay = 0.2f;
    } 
}
