using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public TMP_Text NumKeysText;

    private bool hasKey = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NumKeysText.text = "No Key";
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupKey()
    {
        hasKey = true;
        NumKeysText.text = "Has Key";
    }

    public bool HasKey()
    {
        return hasKey; 
    }
}
