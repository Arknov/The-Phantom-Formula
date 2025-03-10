using UnityEngine;

public class PewPew : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset = new Vector3(1f, 0f, 0f); // Offset to move the gun to the right
    public Vector3 gunScale = new Vector3(5f, 5f, 1f); // Scale to make the gun larger

    private void Start()
    {
        // Set the initial scale of the gun
        transform.localScale = gunScale;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        // Make the gun follow the player with an offset to the right
        transform.position = playerTransform.position + offset;

        // Rotate the gun towards the mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Flip the gun over the y-axis if it rotates past 90 degrees or -90 degrees
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(gunScale.x, -gunScale.y, gunScale.z);
        }
        else
        {
            transform.localScale = gunScale;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = firePoint.right * bulletSpeed;
    }
}

