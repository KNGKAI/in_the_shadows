using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menuObject;
    public GameObject playObject;
    public GameObject testObject;

    public Button ftButton;
    public Button elephantButton;
    public Button globeButton;
    public Button teapotButton;

    private void Awake()
    {
        Switch(0);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
        }
        teapotButton.interactable = true;
        elephantButton.interactable = PlayerPrefs.GetInt("Teapot") == 1;
        globeButton.interactable = PlayerPrefs.GetInt("Elephant") == 1;
        ftButton.interactable = PlayerPrefs.GetInt("Globe") == 1;
    }

    public void SwitchToMenu()
    {
        Switch(0);
    }

    public void SwitchToPlay()
    {
        Switch(1);
    }

    public void SwitchToTest()
    {
        Switch(2);
    }

    private void Switch(int active)
    {
        menuObject.SetActive(false);
        playObject.SetActive(false);
        testObject.SetActive(false);
        switch (active)
        {
            case 0: menuObject.SetActive(true); break;
            case 1: playObject.SetActive(true); break;
            case 2: testObject.SetActive(true); break;
            default: break;
        }
    }
}
