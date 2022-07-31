using UnityEngine;

namespace Controllers
{
    public class ConveyorAnimationController : MonoBehaviour
    {
        #region SelfVariables

        #region Serialized Variables
        [SerializeField]float scrollSpeed = 0.1f;

        

        #endregion
        #region Private Variables
            private Renderer rend;
        

        #endregion

        #endregion
       
        void Awake()
        {
            rend =gameObject.GetComponent<Renderer>();
        


        }

   
        void Update()
        {
            float offset = Time.time*scrollSpeed;
            rend.material.SetTextureOffset("_BaseMap", new Vector2(0, offset));
        
        }
    }
}