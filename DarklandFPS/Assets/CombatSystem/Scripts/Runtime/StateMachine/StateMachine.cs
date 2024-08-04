using UnityEngine;

namespace CombatSystem.StateMachine
{    
    public class StateMachine : MonoBehaviour
    {
        private State currentState;

        private void Update()
        {
            currentState?.Tick(Time.deltaTime);
        }

        protected void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}