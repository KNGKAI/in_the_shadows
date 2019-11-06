using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public void LoadFortyTwoLevel()
    {
        SceneManager.LoadScene("42");
    }

    public void LoadElephant()
    {
        SceneManager.LoadScene("Elephant");
    }

    public void LoadGlobe()
    {
        SceneManager.LoadScene("Globe");
    }

    public void LoadTeapot()
    {
        SceneManager.LoadScene("Teapot");
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void QuitGame()
    {
        Application.Quit();
    }
}
