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
        animator.SetBool("isRunning", state == PlayerState.Running);
        
    }
}
