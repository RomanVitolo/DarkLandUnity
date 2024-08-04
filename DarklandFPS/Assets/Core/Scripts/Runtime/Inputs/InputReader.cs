using System;
using UnityEngine;
using UnityEngine.InputSystem;       

namespace Core.InputReader
{
    [CreateAssetMenu(menuName = "Core/Inputs", fileName = "New Input Reader", order = 0)]
    public class InputReader : ScriptableObject, Controls.IPlayerActions
    {
        public Vector2 MovementValue { get; private set; }
        public Vector2 MovementRotation { get; private set; }
        
        public event Action OnJumpEvent;   
        
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
            MovementRotation = context.ReadValue<Vector2>();
        }

        public void DestroyControls() => _controls.Player.Disable();      
    }
}