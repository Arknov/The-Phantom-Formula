using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        // Find the enemies object by tag and get their colliders
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Ignore collision between the bullet and the enemies
        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                Collider2D enemyCollider = enemy.GetComponent<Collider2D>();
                if (enemy != null)
                {
                    Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemyCollider);
                }
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
        if (collision.CompareTag("Player")) // If the bullet hits the player
        {
            playerManager.Die();
            Destroy(gameObject); // Destroy bullet
        }

        if (collision.CompareTag("Wall")) // Destory bullet if it hits a wall
        {
            Destroy(gameObject);
        }
    }
}

