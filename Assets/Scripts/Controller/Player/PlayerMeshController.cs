using System.Collections;
using UnityEngine;

namespace Controllers
{
    public class PlayerMeshController : MonoBehaviour
    {

        
        public void ChangePlayer(string buttonName)
        {
            SkinnedMeshRenderer smr = transform.GetComponent<SkinnedMeshRenderer>();


            Mesh charMesh = Resources.Load<Mesh>($"Prefabs/CharMesh/{buttonName}");


            smr.sharedMesh = charMesh;

            Debug.Log("de");

        }
       
    }
}