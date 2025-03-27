using Unity.VisualScripting;
using UnityEngine;

public class Craft : MonoBehaviour
{
    [SerializeField] private PermInventoryManager inventory;
    
    [SerializeField] private int electronicsReq;
    [SerializeField] private int metalReq;

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
        if (inventory.getItemCount("electronics") >= electronicsReq && inventory.getItemCount("metal") >= metalReq)
        {
            Debug.Log("success");
            inventory.removeItems("electronics", electronicsReq);
            inventory.removeItems("metal", metalReq);
        } else
        {
            Debug.Log("failure, not enough resources");
        }
    }
}
