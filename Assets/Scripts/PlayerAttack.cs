using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;

    [Header("Attack Settings")]
    [SerializeField] private float attackDuration = 0.35f;
    private bool isAttacking = false;
    private void OnAttack()
    {
        if (isAttacking) return;

        if (stateMachine.TrySetState(PlayerState.Attacking))
        {
            StartCoroutine(AttackRoutine());
        }
    }
    
    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        stateMachine.TrySetState(PlayerState.Idle);
    }
}
