using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelSelectorButton : MonoBehaviour
{
    public int level;
    public TMP_Text levelText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelText.text = level.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenScene()
    {
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
