using UnityEngine;

public class Key : MonoBehaviour
{
    public string color;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<TempInventoryManager>().PickupKey(color);
            Destroy(gameObject);
        }
    }
}
