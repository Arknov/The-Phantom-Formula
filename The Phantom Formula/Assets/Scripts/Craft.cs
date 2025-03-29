using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private int electronicsReq;
    [SerializeField] private int metalReq;
    [SerializeField] private string itemName;

    [SerializeField] private CraftingTable craftingTable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CraftItem()
    {
        //access the persistent perminventorymanager script
        PermInventoryManager inventory = PermInventoryManager.Instance;

        if (inventory.getItemCount("electronics") >= electronicsReq && inventory.getItemCount("metal") >= metalReq)
        {
            Debug.Log("successfully crafted");
            inventory.removeItems("electronics", electronicsReq);
            inventory.removeItems("metal", metalReq);
            inventory.addItems(itemName, 1);
            craftingTable.UpdateText();
        } else
        {
            Debug.Log("failure, not enough resources");
        }
    }
}
