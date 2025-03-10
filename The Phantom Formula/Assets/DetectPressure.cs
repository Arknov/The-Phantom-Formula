using UnityEngine;

public class DetectPressure : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) // Check if the collider belongs to a block
        {
            // Change the color of the pressure plate to red
            spriteRenderer.color = Color.red;
        }
    }

    // Called when another collider exits the trigger collider attached to this object
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block")) // Check if the collider belongs to a block
        {
            // Change the color of the pressure plate back to its original color (white)
            spriteRenderer.color = Color.white;
        }
    }
}

