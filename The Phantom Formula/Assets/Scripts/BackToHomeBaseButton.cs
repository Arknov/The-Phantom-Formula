using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToHomeBaseButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadHomeBase()
    {
        SceneManager.LoadScene("HomeBase");
    }
}
