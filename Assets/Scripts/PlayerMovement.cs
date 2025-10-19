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
        if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            stateMachine.SetState(PlayerState.Running);
        }
        else
        {
            stateMachine.SetState(PlayerState.Idle);
        }
    }

    private void FixedUpdate() {
        _rb.linearVelocity = _moveInput.normalized * speed;
    }
}
