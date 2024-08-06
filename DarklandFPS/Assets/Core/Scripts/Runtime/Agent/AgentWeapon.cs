using UnityEngine;

namespace Core.Scripts.Runtime.Agent
{
    public enum WeaponType
    {
       Melee,
       Pistol,
       Rifle,
       Sniper
    }
    
    public abstract class BaseWeaponStats : ScriptableObject
    {
        [field: SerializeField] protected WeaponType WeaponType { get; set; }
        [field: SerializeField] protected float FireRate { get; set; }
        [field: SerializeField] protected int BulletAmount { get; set; }     
    }
    
    [CreateAssetMenu(menuName = "Core/Agent/AgentWeapon", fileName = "New Weapon")]
    public class AgentWeapon : BaseWeaponStats
    {
        
    }
}