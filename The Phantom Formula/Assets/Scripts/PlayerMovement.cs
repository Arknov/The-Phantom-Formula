using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Freeze the rotation to prevent the player from rotating due to physics
        rb.freezeRotation = true;
        
        //animator = GetComponent<Animator>();

    }

    void Update()
    {
        // Get input from player
        movement.x = Input.GetAxisRaw("Horizontal");  // A = left, D = right
        movement.y = Input.GetAxisRaw("Vertical");    // W = up, S = down

        // Normalize movement to prevent faster diagonal movement
        movement = movement.normalized;
        /*
        if (movement.x > 0)
        {
            animator.Play("moveRight");
        }
        else if (movement.x < 0)
        {
            animator.Play("moveLeft");
        }
        else if (movement.y > 0)
        {
            animator.Play("moveUP");
        }
        else if (movement.y < 0)
        {
            animator.Play("moveDown");
        }
        */
    }

    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }
}
