using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public abstract class BaseAgentMovement : ScriptableObject
    {
        [field: SerializeField] protected float freeLookMovementSpeed { get; set; }
        [field: SerializeField] protected float smoothRotationSpeed { get; private set; }
        [field: SerializeField] protected float speedChangeRate { get;  set; }
        [field: SerializeField] protected float sprintSpeed { get;  set; } 
    }
    
    [CreateAssetMenu(menuName = "Core/Agent/AgentMovement", fileName = "AgentMovement")]
    public class AgentMovement : BaseAgentMovement
    {
        [HideInInspector] public CharacterController CharacterController;   
        public float MovementSpeed => freeLookMovementSpeed;
        public float RotationSpeed => smoothRotationSpeed;   
        public float SpeedChangeRate => speedChangeRate;   
        public float SprintSpeed => sprintSpeed;       
    } 
}      