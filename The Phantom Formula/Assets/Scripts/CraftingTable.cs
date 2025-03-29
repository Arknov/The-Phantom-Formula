using TMPro;
using UnityEngine;

public class CraftingTable : MonoBehaviour
{
    private PermInventoryManager inventory;
    [SerializeField] private GameObject TextPopUp;
    [SerializeField] private GameObject CraftingScreen;
    [SerializeField] private TMP_Text MetalCountText;
    [SerializeField] private TMP_Text ElectronicsCountText;

    private bool playerInBox = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = PermInventoryManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox && Input.GetKeyDown(KeyCode.F))
        {
            CraftingScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInBox = true;
            UpdateText();
            TextPopUp.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInBox = false;
            TextPopUp.SetActive(false);
            CraftingScreen.SetActive(false);
        }
    }
    
    public void UpdateText()
    {
        MetalCountText.text = "Metal: " + inventory.getItemCount("metal").ToString();
        ElectronicsCountText.text = "Electronics: " + inventory.getItemCount("electronics").ToString();
    }
}
