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

    public Animator animator;

    public bool canMove = true;

    public static PlayerController Instance { get; private set; }    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        animator.SetFloat("x", movement.y == 0 ? movement.x : 0);
        animator.SetFloat("y", movement.y);

        if (canMove == false) {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = movement * moveSpeed;

        currentVelocity = Vector2.MoveTowards(
            currentVelocity,
            targetVelocity,
            (movement.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime
        );

        rb.linearVelocity = currentVelocity;
    }
}
