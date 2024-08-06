using Cinemachine;
using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public abstract class BaseAgentCamera : ScriptableObject
    {
        protected float _cineMachineTargetPitch;
        protected float _rotationVelocity;
    }
    
    [CreateAssetMenu(menuName = "Core/Agent/Agent Camera", fileName = "AgentCamera")]
    public class AgentCamera : BaseAgentCamera
    {
        public CinemachineVirtualCamera VirtualCamera { get; set; }
        public GameObject CameraTarget { get; set; }
        public Camera MainCamera { get; set; }  
        public float CineMachineTargetPitch
        {
            get => _cineMachineTargetPitch;
            set => _cineMachineTargetPitch = value;
        }
        
        public float RotationVelocity
        {
            get => _rotationVelocity;
            set => _rotationVelocity = value;
        }
    }        
}
