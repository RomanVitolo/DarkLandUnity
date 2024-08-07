using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Scripts.Runtime.Agent
{   
    public class Agent : MonoBehaviour
    {
        [field: SerializeField] public AgentHealth AgentHealth { get; set; }
        [field: SerializeField] public InputReader.InputReader InputReader { get; private set; }
        [field: SerializeField] public AgentMovement AgentMovement { get; private set; }
        [field: SerializeField] public AgentCamera AgentCamera { get; private set; }
        [field: SerializeField] public AgentAnimator AgentAnimator { get; private set; }

        private void Awake()
        {
            InitializeComponents();
        }
        
        private void InitializeComponents()
        {
            InputReader.InitializeControls();
            AgentMovement.CharacterController = GetComponent<CharacterController>();
            AgentAnimator.Animator = GetComponent<Animator>();
            AgentCamera.MainCamera = Camera.main;
            AgentCamera.CameraTarget = GameObject.FindWithTag("CinemachineTarget");
            AgentCamera.VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            _playerInput = GetComponent<PlayerInput>();
        }
        
        #if ENABLE_INPUT_SYSTEM
        [SerializeField] private PlayerInput _playerInput;
        #endif
        
        public bool IsCurrentDeviceMouse
        {
            get
            {
                #if ENABLE_INPUT_SYSTEM
                    return _playerInput.currentControlScheme == "KeyboardMouse";
                #else
                    return false;
                #endif
            }
        }
    }          
}
