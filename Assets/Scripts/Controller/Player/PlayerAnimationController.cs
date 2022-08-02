using UnityEngine;
using Enums;
namespace Controllers
{
    public class PlayerAnimationController : MonoBehaviour
    {
        #region SelfVariables

        #region Public Variables

        #endregion
        #region Serialized Variables

        [SerializeField] private Animator playerAnimator;
        

        #endregion
        #region Private Variables

        

        #endregion

        #endregion
        public void ChangeAnimationState(PlayerAnimationStates currentState)
        {
            switch(currentState)
            {
                case PlayerAnimationStates.Idle:
                    playerAnimator.SetTrigger("Idle");
                    break;
                case PlayerAnimationStates.Walk:
                    playerAnimator.SetTrigger("Walk");
                    break;                
                case PlayerAnimationStates.Dance:
                    playerAnimator.SetTrigger("Dance");
                    break;
            }
            
        }
        
    }
}