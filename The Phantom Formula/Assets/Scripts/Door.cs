using UnityEngine;

//VERY TEMPORARY

public class Door : MonoBehaviour
{
    public Sprite OpenDoor;

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
            TempInventoryManager inventory = collision.GetComponent<TempInventoryManager>();

            if (inventory.HasKey(color))
            {
                GetComponent<SpriteRenderer>().sprite = OpenDoor;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
