using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        // Animator and SpriteRenderer are usually on the child sprite
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // 1. Get input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // Calculate normalized movement
        movement = new Vector2(moveX, moveY).normalized;

        // Update animator parameters
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("isMoving", movement.sqrMagnitude > 0);

        // Flip sprite only for horizontal movement
        if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
        {
            spriteRenderer.flipX = movement.x < 0; // left/right flip
        }
        else
        {
            spriteRenderer.flipX = false; // no flip for up/down
        }
    }

    void FixedUpdate()
    {
        // Move player
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}

