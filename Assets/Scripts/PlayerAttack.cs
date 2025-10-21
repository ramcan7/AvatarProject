using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine stateMachine;
   private void OnAttack()
    {
        if (!stateMachine.Is(PlayerState.Attacking))
            StartCoroutine(AttackRoutine());
    }
    
    private IEnumerator AttackRoutine()
    {
        stateMachine.SetState(PlayerState.Attacking);
        yield return new WaitForSeconds(0.35f);
        stateMachine.SetState(PlayerState.Idle);
    }
}
