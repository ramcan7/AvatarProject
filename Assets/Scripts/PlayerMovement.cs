using System.Runtime.InteropServices.WindowsRuntime;
using NUnit.Framework;
using Unity.Collections;
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
    [SerializeField] private float jumpImpulse = 80f;
    [SerializeField] private float raycastDistance = 0.2f;
    [SerializeField] private bool stopMomentumOnAttack = true;
    [SerializeField] private Transform foot;
    [SerializeField] private LayerMask groundMask;
    private bool wasGroundedLastFrame = true;
    private Vector2 _moveInput;
    private int currentFacingDirection = 1;
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

        if (_moveInput.x != 0)
        {
            int newDirection = _moveInput.x > 0 ? 1 : -1;
            if(newDirection != currentFacingDirection)
            {
                currentFacingDirection = newDirection;
                FlipPlayer();
            }
        }

        UpdateMovementState();
    }

    private void OnJump()
    {
        if(CheckGrounding() && !stateMachine.Is(PlayerState.Jumping))
        {
            stateMachine.TrySetState(PlayerState.Jumping);
            wasGroundedLastFrame = true;
        }
    }

    private void UpdateMovementState()
    {
        // attack has priority
        if (stateMachine.Is(PlayerState.Attacking)) return;
        if (stateMachine.Is(PlayerState.Jumping)) return;

        PlayerState targetState = (_moveInput.sqrMagnitude > 0.01f)
            ? PlayerState.Running
            : PlayerState.Idle;

        stateMachine.TrySetState(targetState);
    }

    private void FixedUpdate()
    {
        Vector2 velocity = new Vector2(_moveInput.x, 0) * speed;
        if (stateMachine.Is(PlayerState.Attacking) && stopMomentumOnAttack)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (stateMachine.Is(PlayerState.Jumping) && CheckGrounding() && !wasGroundedLastFrame)
        {
            stateMachine.TrySetState(PlayerState.Idle);
        }

        if (stateMachine.Is(PlayerState.Jumping) && wasGroundedLastFrame)
        {
            velocity.y = jumpImpulse;
            wasGroundedLastFrame = !wasGroundedLastFrame;
        }


        rb.linearVelocity = velocity;
    }

    private bool CheckGrounding()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(foot.position, Vector2.down, raycastDistance, groundMask);
        return hit;
    }

    private void FlipPlayer()
    {
        Vector3 scale = transform.localScale;
        scale.x = currentFacingDirection;
        transform.localScale = scale;
    }
}
