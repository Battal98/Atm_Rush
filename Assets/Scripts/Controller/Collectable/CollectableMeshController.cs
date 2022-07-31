using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {

        [SerializeField]
        private MeshFilter m_Mesh;

        private void Awake()
        {
            m_Mesh = GetComponentInChildren<MeshFilter>();
        }
        public void UpdateCollectableMesh(ref CollectableType _type, List<Mesh> _list)
        {
            if ((int)_type < _list.Count-1)
            {
                _type++;
                m_Mesh.mesh = _list[(int)_type];
            }
        }
    } 
}
