using UnityEngine;

namespace CombatSystem.StateMachine.PlayerState
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine _playerStateMachine;

        protected PlayerBaseState(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;   
        }
    }
}