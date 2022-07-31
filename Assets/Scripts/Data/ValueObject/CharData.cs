using UnityEngine;
using System;
using UnityEngine.UI;

namespace Data.ValueObject
{  
    [Serializable]
    public class CharData
    {
        public string CharName;
        public int CharButtonID;
        public int CharPrice;
        public Button CharButton;
        public GameObject Lock;
    } 
}
