using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Freeze the rotation to prevent the player from rotating due to physics
        rb.freezeRotation = true;

        // Adjust the sprite to face up by default (90 degrees counterclockwise from the right)
        transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    void Update()
    {
        // Get input from player
        movement.x = Input.GetAxisRaw("Horizontal");  // A = left, D = right
        movement.y = Input.GetAxisRaw("Vertical");    // W = up, S = down

        // Normalize movement to prevent faster diagonal movement
        movement = movement.normalized;

        // Snap the sprite's rotation directly to the movement direction
        if (movement.sqrMagnitude > 0)
        {
            // Calculate angle of movement direction
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Adjust the target angle to account for initial 90-degree clockwise rotation of the sprite
            targetAngle -= 360f;  // Adjust for default sprite's 90-degree clockwise rotation

            // Set the rotation directly to make the sprite face the correct direction
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        }
    }

    void FixedUpdate()
    {
        // Apply movement using Rigidbody2D
        rb.linearVelocity = movement * moveSpeed;
    }
}
