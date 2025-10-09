using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 moveInput;
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate() {
        rb.linearVelocity = moveInput.normalized * speed;
    }
}
