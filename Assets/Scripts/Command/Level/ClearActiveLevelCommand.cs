using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public class ClearActiveLevelCommand:MonoBehaviour
    {   
        public void LevelClearer(Transform levelHolder)
        {
            Destroy(levelHolder.GetChild(0).gameObject);
        }

    } 
}
