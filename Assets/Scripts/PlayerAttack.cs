using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine stateMachine;
    private void OnAttack() {
        // TODO: ATTACK LOGIC
        stateMachine.ChangeState(PlayerState.Attacking);
    }
}
