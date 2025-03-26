using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;
    private Collider2D playerCollider;

    private void Start()
    {
        // Find the player object by tag and get its collider
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerCollider = player.GetComponent<Collider2D>();
            if (playerCollider != null)
            {
                // Ignore collision between the bullet and the player
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerCollider);
            }
        }

        // Destroy bullet after X seconds
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // If the bullet hits an enemy
        {
            Destroy(collision.gameObject); // Destroy enemy
            Destroy(gameObject); // Destroy bullet
        }

        if (collision.CompareTag("Wall")) // Destory bullet if it hits a wall
        {
            Destroy(gameObject);
        }
    }
}

