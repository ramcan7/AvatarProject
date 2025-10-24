using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private Rigidbody2D rb;

    [Header("MovementSettings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool stopMomentumOnAttack = true;
    private Vector2 _moveInput;
    
    private void Awake() {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        UpdateMovementState();
    }

    private void UpdateMovementState()
    {
        // attack has priority
        if (stateMachine.Is(PlayerState.Attacking)) return;

        PlayerState targetState = (_moveInput.sqrMagnitude > 0.01f)
            ? PlayerState.Running
            : PlayerState.Idle;

        stateMachine.TrySetState(targetState);
    }

    private void FixedUpdate() 
    {
        if (stateMachine.Is(PlayerState.Attacking) && stopMomentumOnAttack)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        };

        rb.linearVelocity = _moveInput.normalized * speed;
    }
}
