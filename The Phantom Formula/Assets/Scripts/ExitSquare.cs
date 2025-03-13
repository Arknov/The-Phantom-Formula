using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitSquare : MonoBehaviour
{
    public GameObject TextPopUp;

    private bool playerInBox = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("HomeBase");
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
        }
    }
}
