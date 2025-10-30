using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private Rigidbody2D rb;

    [Header("MovementSettings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpImpulse = 5f;
    [SerializeField] private float raycastDistance = 0.2f;
    [SerializeField] private bool stopMomentumOnAttack = true;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask groundMask;
    private Vector2 _moveInput;
    private bool hasJumped = false;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.DrawRay(foot.position, Vector2.down * raycastDistance, Color.red);
    }

    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
        UpdateMovementState();
    }

    private void OnJump()
    {
        if(CheckGrounding())
        {
            hasJumped = stateMachine.TrySetState(PlayerState.Jumping);
        }
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
        }

        if(stateMachine.Is(PlayerState.Jumping) && hasJumped)
        {
            rb.linearVelocityY = jumpImpulse;
            return;
        }

        rb.linearVelocity = _moveInput.normalized * speed;
    }

    private bool CheckGrounding()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(foot.position, Vector2.down, raycastDistance, groundMask);
        return hit;
    }
}
