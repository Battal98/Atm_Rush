using System;
using Data.ValueObject;
using UnityEngine;
using Keys;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region SelfVariables

        #region SerializedVariables
        [SerializeField] private new Rigidbody rigidbody;
        

        #endregion

        #region PublicVariables

        

        #endregion

        #region PrivateVariables
        [Header("Movement Data")]private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay;
        private float _inputValue;
        private Vector2 _clampValues;
        #endregion
        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            
            _movementData=dataMovementData;
            
            //Debug.Log("_movementData"+_movementData.SidewaysSpeed);
        }

        public void EnableMovement()
        {
            //Debug.Log("_isReadyToMove"+_isReadyToMove+"_isReadyToPlay"+_isReadyToPlay);
            _isReadyToMove = true;
            _isReadyToPlay = true;//Deneme
        }
        public void DisableMovement()
        {
            _isReadyToMove = false;
        }
        public void UpdateInputValue(HorizontalInputParams inputParam)
        {
            _inputValue=inputParam.XValue;
            _clampValues=inputParam.ClampValues;
        }
        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isReadyToMove)
                {
                    Move();
                }
                else
                {
                    StopSideways();
                }
            }
            else
                Stop();
        }

        private void Move()
        {
           
            var velocity = rigidbody.velocity;
            //Debug.Log("_movementData"+_movementData.SidewaysSpeed);
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed, velocity.y,
                _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }
        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}