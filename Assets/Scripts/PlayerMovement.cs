using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine stateMachine;
    private Vector2 _moveInput;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D _rb;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value) {
        stateMachine.ChangeState(PlayerState.Moving);
        _moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate() {
        _rb.linearVelocity = _moveInput.normalized * speed;
    }
}
