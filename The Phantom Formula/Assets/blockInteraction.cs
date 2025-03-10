using UnityEngine;

public class blockInteraction : MonoBehaviour
{
    public float frictionCoefficient = 0.1f; // Coefficient of linear friction
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the GameObject.");
        }
        else
        {
            // Freeze all rotational motion
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // FixedUpdate is called at a fixed interval and is used for physics updates
    void FixedUpdate()
    {
        ApplyFriction();
    }

    void ApplyFriction()
    {
        if (rb != null)
        {
            // Calculate the linear friction force
            Vector2 frictionForce = -rb.linearVelocity.normalized * frictionCoefficient * rb.mass;

            // Apply the linear friction force
            rb.AddForce(frictionForce);
        }
    }
}



