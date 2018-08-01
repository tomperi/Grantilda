using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool creditsDisplayed;
    public bool levelSelectDisplayed;

    public void QuitGame()
    {
        Application.Quit();
    }
}
