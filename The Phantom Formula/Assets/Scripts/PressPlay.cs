using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PressPlay : MonoBehaviour
{
    public Button PlayButton; // Reference to the Play button

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayButton != null)
        {
            PlayButton.onClick.AddListener(taskOnClick);
        }
        else
        {
            Debug.LogError("PlayButton is not assigned in the Inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void taskOnClick()
    {
        Debug.Log("You have clicked the button!");
        SceneManager.LoadScene(sceneName: "SampleScene");
    }
}