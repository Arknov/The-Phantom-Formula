using UnityEngine;

//VERY TEMPORARY

public class Door : MonoBehaviour
{
    public Sprite OpenDoor;

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
            InventoryManager inventory = collision.GetComponent<InventoryManager>();

            if (inventory.HasKey())
            {
                this.GetComponent<SpriteRenderer>().sprite = OpenDoor;
                this.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
