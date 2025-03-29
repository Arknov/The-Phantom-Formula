using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [SerializeField] private string itemType;
    [SerializeField] private int itemCount;

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
            PermInventoryManager.Instance.addItems(itemType, itemCount);
            Destroy(gameObject);
        }
    }
}
