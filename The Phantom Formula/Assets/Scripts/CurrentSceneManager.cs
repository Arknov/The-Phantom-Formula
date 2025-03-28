using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentSceneManager : MonoBehaviour
{

    [SerializeField] private GameObject DeathPanel;

    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadHomeBase()
    {
        SceneManager.LoadScene("HomeBase");
    }

    public void KillPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        FindDeathPanel().SetActive(true);
    }

    private GameObject FindDeathPanel()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>(); // Finds all objects, including inactive ones

        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Death Panel") && !obj.activeInHierarchy) // Checks if it matches the tag and is inactive
            {
                return obj;
            }
        }
        Debug.Log("No Death Panel found");
        return null; // Returns null if no inactive object is found
    }
}
