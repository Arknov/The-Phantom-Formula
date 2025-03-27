using System.Collections.Generic;
using UnityEngine;

public class PermInventoryManager : MonoBehaviour
{
    //dictionary pairs each item type with an item count
    private Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static PermInventoryManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initialize each item type at zero
        inventory.Add("electronics", 0);
        inventory.Add("metal", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //for testing purposes
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(inventory["metal"] + " metal");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            addItems("metal", 100);
            Debug.Log("Debug: +100 metal");
        }
        //^^^^^^^^^^^^^^^^^^^^^^
    }

    //add items to the iventory
    public void addItems(string itemType, int numItems)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType] = inventory[itemType] + numItems;
        } else
        {
            Debug.Log("Tried to access item type " + itemType + " which does not exist");
        }
    }

    //add items to the iventory
    public void removeItems(string itemType, int numItems)
    {
        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType] = inventory[itemType] - numItems;
        }
        else
        {
            Debug.Log("Tried to access item type " + itemType + " which does not exist");
        }
    }

    //returns the amount of a given item type
    public int getItemCount(string itemType)
    {
        if (inventory.ContainsKey(itemType))
        {
            return inventory[itemType];
        }
        else
        {
            Debug.Log("Tried to access item type " + itemType + " which does not exist");
            return 0;
        }
    }
}
