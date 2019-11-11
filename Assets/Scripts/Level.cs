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

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void LoadMenuStatic()
    {
        SceneManager.LoadScene("Menu");
    }

    public static void QuitGameStatic()
    {
        Application.Quit();
    }
}
