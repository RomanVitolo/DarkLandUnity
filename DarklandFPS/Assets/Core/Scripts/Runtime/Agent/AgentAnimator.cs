using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public abstract class BaseAgentAnimator : ScriptableObject
    {
        [field: SerializeField] protected readonly int _freeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");   
    }
    
    [CreateAssetMenu(menuName = "Core/Agent/AgentAnimator", fileName = "AgentAnimator")]
    public class AgentAnimator : BaseAgentAnimator
    { 
        public Animator Animator { get; set; }        
        public int FreeLookSpeedAnim => _freeLookSpeedHash;    
    }
}