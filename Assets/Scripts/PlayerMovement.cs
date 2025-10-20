using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private float speed = 5f;
    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value) {
        _moveInput = value.Get<Vector2>();
        PlayerState state;
        if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            state = PlayerState.Running;
        }
        else
        {
            state = PlayerState.Idle;
        }
        
        if(stateMachine.CanTransitionTo(state))
            stateMachine.SetState(state);
    }

    private void FixedUpdate() {
        _rb.linearVelocity = _moveInput.normalized * speed;
    }
}
