using System;
using Cinemachine;
using UnityEngine;  
using Core.InputReader;

namespace CombatSystem.StateMachine.PlayerState
{
    public class PlayerStateMachine : StateMachine
    {
        [field: SerializeField] public InputReader InputReader { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator PlayerAnimator { get; private set; }
        [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
        [field: SerializeField] public Transform MainCameraTransform { get; private set; }

        private void Awake()
        {
            InitializeComponents();
        }

        private void Start()
        {
            SwitchState(new PlayerFreeLookState(this));
        }

        private void OnDestroy()
        {
            InputReader.DestroyControls();
        }


        private void InitializeComponents()
        {
            InputReader.InitializeControls();
            CharacterController = GetComponent<CharacterController>();
            PlayerAnimator = GetComponent<Animator>();
            MainCameraTransform = Camera.main.transform;
        }
    }
}