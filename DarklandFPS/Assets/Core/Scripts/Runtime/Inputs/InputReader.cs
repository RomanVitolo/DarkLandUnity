using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Core.InputReader
{
    [CreateAssetMenu(menuName = "Core/Inputs", fileName = "New Input Reader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        [field: SerializeField] public bool CursorLocked { get; private set; } = true; 
        [field: SerializeField] public bool CursorInputForLook { get; private set; } = true;
        public Vector2 MovementValue { get; private set; }
        public Vector2 MovementRotation { get; private set; }
        public Vector2 MousePosition { get; private set; }
        
        public event Action OnJumpEvent;   
        public bool AnalogMovement;   
        
        [field : SerializeField] public bool IsSprintPressed { get; private set; }   
        private Controls _controls;       
        
        public void InitializeControls()
        {
            _controls = new Controls();
            _controls.Player.SetCallbacks(this);        
            _controls.Player.Enable();
        }                    
        
        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            
            OnJumpEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementValue = context.ReadValue<Vector2>();
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (CursorInputForLook)  
                MovementRotation = context.ReadValue<Vector2>();  
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (!context.ReadValueAsButton()) 
                IsSprintPressed = false;
            else            
                SprintInput(context.performed);
        }    
        
        private void SprintInput(bool newSprintState)
        {
            IsSprintPressed = newSprintState;
        }
        
        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }                  

        public void DestroyControls() => _controls.Player.Disable();      
    }
}