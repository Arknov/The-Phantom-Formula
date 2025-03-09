using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime); // Destroy bullet after X seconds
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
    }
}
