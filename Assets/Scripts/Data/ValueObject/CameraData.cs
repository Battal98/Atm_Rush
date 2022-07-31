using System.Collections.Generic;
using UnityEngine;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class CameraData 
    {

        public Vector3 cameraOnPlayRot = new Vector3(19,0,0);

        public float _camYPosition = 2;

        public List<Transform> camPathTargets = new List<Transform>();
    } 
}
