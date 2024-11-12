using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 50f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 currentVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized; // Normalize to prevent faster diagonal movement
    }

    private void FixedUpdate()
    {
        // Calculate target velocity
        Vector2 targetVelocity = movement * moveSpeed;

        // Smoothly adjust current velocity towards target velocity
        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            (movement.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        // Apply movement
        rb.linearVelocity = currentVelocity;
    }
}
