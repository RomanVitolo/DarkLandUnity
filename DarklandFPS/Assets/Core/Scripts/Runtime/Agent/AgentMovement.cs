using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public abstract class BaseAgentMovement : ScriptableObject
    {
        [field: SerializeField] protected float FreeLookMovementSpeed { get; set; }
        [field: SerializeField] protected float SmoothRotationSpeed { get; private set; } 
    }
    
    [CreateAssetMenu(menuName = "Core/Agent/AgentMovement", fileName = "AgentMovement")]
    public class AgentMovement : BaseAgentMovement
    {
        [HideInInspector] public CharacterController CharacterController;   
        public float MovementSpeed => FreeLookMovementSpeed;
        public float RotationSpeed => SmoothRotationSpeed;     
    } 
}      