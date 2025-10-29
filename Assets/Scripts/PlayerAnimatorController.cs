using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStateMachine stateMachine;

    private void OnEnable()
    {
        stateMachine.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        stateMachine.OnStateChanged -= HandleStateChanged;
    }
    
    private void HandleStateChanged(PlayerState state)
    {
        Debug.Log($"Animator reacting to state: {state}");
        animator.SetBool("isAttacking", state == PlayerState.Attacking);
        animator.SetBool("isRunning", state == PlayerState.Running);
    }
}
