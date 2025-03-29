using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempInventoryManager : MonoBehaviour
{
    public TMP_Text NumKeysText;

    private bool hasYellowKey = false;
    private bool hasBlueKey = false;
    private bool hasRedKey = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "HomeBase")
        {
            NumKeysText.text = "No Key";
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickupKey(string color)
    {
        if (NumKeysText != null)
        {
            if (color == "yellow")
            {
                hasYellowKey = true;
                NumKeysText.text = "Collected Yellow Key";
            } else if (color == "blue")
            {
                hasBlueKey = true;
                NumKeysText.text = "Collected Blue Key";
            } else if (color == "red")
            {
                hasRedKey = true;
                NumKeysText.text = "Collected Red Key";
            } else
            {
                Debug.LogWarning("Tried to collect a key color that does not exist");
            }
        } else
        {
            Debug.LogWarning("Tried to pickup a key, but there is no HasKeyText");
        }
    }

    public bool HasKey(string color)
    {
        if (color == "yellow")
        {
            return hasYellowKey;
        }
        else if (color == "blue")
        {
            return hasBlueKey;
        }
        else if (color == "red")
        {
            return hasRedKey;
        } else
        {
            Debug.LogWarning("Tried to check for a key color that does not exist");
            return false;
        }
    }
}
