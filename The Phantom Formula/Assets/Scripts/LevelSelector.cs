using UnityEngine;
public class LevelSelector : MonoBehaviour
{
    public GameObject TextPopUp;
    public GameObject LevelSelectScreen;

    private bool playerInBox = false; //player is inside the collider

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox && Input.GetKeyDown(KeyCode.F))
        {
            LevelSelectScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInBox = true;
            TextPopUp.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInBox = false;
            TextPopUp.SetActive(false);
            LevelSelectScreen.SetActive(false);
        }
    }
}
