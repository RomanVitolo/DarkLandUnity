using System;
using UnityEngine;

namespace Core.Scripts.Runtime.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -90.0f;
        
        public float SpeedChangeRate = 10.0f;
        public float SprintSpeed = 6.0f;
        public float MoveSpeed = 4.0f;
        private float _speed;
        private float _verticalVelocity;
        [field: SerializeField] public Agent.Agent Agent { get; private set; }     
       
        private const float _threshold = 0.01f;
        
        private void Update()
        {  
            PlayerMovement();   
            
            PlayerAnimations(Time.deltaTime);
        }                         

        private void LateUpdate() => CameraRotation();          
        

        private void CameraRotation()
        {
            if (Agent.InputReader.MovementRotation.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime
                float deltaTimeMultiplier = Agent.IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
				
                Agent.AgentCamera.CineMachineTargetPitch += Agent.InputReader.MovementRotation.y * Agent.AgentMovement.RotationSpeed * deltaTimeMultiplier;
                Agent.AgentCamera.RotationVelocity = Agent.InputReader.MovementRotation.x * Agent.AgentMovement.RotationSpeed * deltaTimeMultiplier;

                // clamp our pitch rotation
                Agent.AgentCamera.CineMachineTargetPitch = ClampAngle(Agent.AgentCamera.CineMachineTargetPitch, BottomClamp, TopClamp);

                // Update Cinemachine camera target pitch
                Agent.AgentCamera.CameraTarget.transform.localRotation = Quaternion.Euler(Agent.AgentCamera.CineMachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * Agent.AgentCamera.RotationVelocity);
            }
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }         

        private void PlayerAnimations(float deltaTime)
        {
            if (Agent.InputReader.MovementValue == Vector2.zero)
            {   
                Agent.AgentAnimator.Animator.SetFloat(Agent.AgentAnimator.FreeLookSpeedAnim, 0, 0.1f, deltaTime);  
            }

            Agent.AgentAnimator.Animator.SetFloat(Agent.AgentAnimator.FreeLookSpeedAnim, 1, 0.1f, deltaTime);
        }                
        
        private void PlayerMovement()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = Agent.InputReader.SprintPressed ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (Agent.InputReader.MovementValue == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(Agent.AgentMovement.CharacterController.velocity.x, 0.0f,
                Agent.AgentMovement.CharacterController.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = Agent.InputReader.AnalogMovement ? Agent.InputReader.MovementValue.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            // normalise input direction
            Vector3 inputDirection = new Vector3(Agent.InputReader.MovementValue.x, 0.0f, Agent.InputReader.MovementValue.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (Agent.InputReader.MovementValue != Vector2.zero)
            {
                // move
                inputDirection = transform.right * Agent.InputReader.MovementValue.x + transform.forward * Agent.InputReader.MovementValue.y;
            }

            // move the player
            Agent.AgentMovement.CharacterController.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        }    
        
        
        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        
        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}