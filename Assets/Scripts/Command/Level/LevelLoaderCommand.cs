using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using Data.ValueObject;

namespace Command
{
    public class LevelLoaderCommand :MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;
        public void LoadLevel(Transform levelHolder,int _levelID)
        {
            if(_levelID >= levelManager.Levels.Count)
            {
                _levelID = 0;
            }
            Instantiate(Resources.Load<GameObject>($"Prefabs/Level/Level {_levelID}"),levelHolder);
        }
    } 
}
