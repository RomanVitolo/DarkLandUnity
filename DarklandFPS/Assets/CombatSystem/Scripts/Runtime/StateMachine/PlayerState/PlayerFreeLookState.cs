﻿using UnityEngine;

namespace CombatSystem.StateMachine.PlayerState
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        
        public PlayerFreeLookState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
        {
        }

        public override void Enter()
        {
            
        }      

        private float xRotation;
        private float YRotation;
        public override void Tick(float deltaTime)
        {    
            Vector3 movement = CalculateMovement();
              
            _playerStateMachine.CharacterController.
                Move(movement * (_playerStateMachine.FreeLookMovementSpeed * deltaTime));

            if (_playerStateMachine.InputReader.MovementValue == Vector2.zero)
            {   
                _playerStateMachine.PlayerAnimator.SetFloat(_freeLookSpeedHash, 0, 0.1f, deltaTime);
                return;
            }

            _playerStateMachine.PlayerAnimator.SetFloat(_freeLookSpeedHash, 1, 0.1f, deltaTime);
            
            xRotation -= _playerStateMachine.InputReader.MovementRotation.y * deltaTime;  
            
            //_playerStateMachine.transform.rotation = Quaternion.LookRotation(movement); 
            _playerStateMachine.transform.Rotate(Vector3.up * _playerStateMachine.InputReader.MovementRotation.x); 
            Debug.Log(_playerStateMachine.InputReader.MovementRotation.x);

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            _playerStateMachine.VirtualCamera.transform.localRotation = Quaternion.Euler(xRotation,0 ,0);     
            
        }

        public override void Exit()
        {
          
        }

        private Vector3 CalculateMovement()
        {
            Vector3 forward = _playerStateMachine.VirtualCamera.transform.forward;
            Vector3 right = _playerStateMachine.VirtualCamera.transform.right;

            forward.y = 0f;
            right.y = 0f;
            
            forward.Normalize();
            right.Normalize();

            return forward * _playerStateMachine.InputReader.MovementValue.y +
                   right * _playerStateMachine.InputReader.MovementValue.x;
        }
    }
}