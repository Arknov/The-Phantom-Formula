using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        deathPanel.SetActive(true);
        gameObject.SetActive(false);
    }

}
