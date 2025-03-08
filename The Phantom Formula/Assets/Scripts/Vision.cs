using UnityEngine;

public class Vision : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f; // Speed at which the object moves towards the player
    private GameObject player; // Reference to the player object

    private bool hasLineOfSight = false; // Flag to check if the object has line of sight to the player

    void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            // Log an error if the player object is not found
            Debug.LogError("Player object not found. Please ensure the player object has the tag 'Player'.");
        }
    }

    void Update()
    {
        // Move towards the player if the object has line of sight and the player is not null
        if (hasLineOfSight && player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            // Cast a ray from the object to the player
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
            if (ray.collider != null)
            {
                // Check if the ray hit the player object
                hasLineOfSight = ray.collider.CompareTag("Player");
                if (hasLineOfSight)
                {
                    // Draw a green ray if the object has line of sight to the player
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {
                    // Draw a red ray if the object does not have line of sight to the player
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                }
            }
        }
    }
}
