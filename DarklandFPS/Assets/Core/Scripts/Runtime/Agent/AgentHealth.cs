using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public abstract class BaseAgentHealth : ScriptableObject
    {
        [field: SerializeField] protected float MaxHealth { get; set; }
        [field: SerializeField] protected float CurrentHealth { get; set; }
    }
    [CreateAssetMenu(menuName = "Core/Agent/Agent Health", fileName = "AgentHealth")]
    public class AgentHealth : BaseAgentHealth
    {     
        
        
    }       
}