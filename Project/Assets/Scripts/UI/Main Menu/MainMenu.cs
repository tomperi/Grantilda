using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private bool creditsDisplayed;
    private bool levelSelectDisplayed;

    public GameObject CreditsMenu;
    public GameObject LevelSelectMenu;
    public GameObject Logo;

    private Animator creditsAnimator;
    private Animator levelsAnimator;
    private Animator logoAnimator;

    public bool Credits
    {
        get
        {
            return creditsDisplayed;
        }

        set
        {
            creditsDisplayed = value;
        }
    }

    public bool Levels
    {
        get
        {
            return levelSelectDisplayed;
        }

        set
        {
            levelSelectDisplayed = value;
        }
    }

    void Start()
    {
        creditsAnimator = CreditsMenu.GetComponent<Animator>();
        levelsAnimator = LevelSelectMenu.GetComponent<Animator>();
        logoAnimator = Logo.GetComponent<Animator>();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLevelSelect()
    {
        if (creditsDisplayed)
        {
            creditsAnimator.SetBool("SlideOut", false);
        }
        else
        {
            levelsAnimator.SetBool("SlideOut", true);
        }

        levelSelectDisplayed = true;
        creditsDisplayed = false;
    }

    public void ShowCredits()
    {
        if (levelSelectDisplayed)
        {
            levelsAnimator.SetBool("SlideOut", false);
        }
        else
        {
            logoAnimator.SetBool("SlideOut", false);
            creditsAnimator.SetBool("SlideOut", true);
        }

        creditsDisplayed = true;
        levelSelectDisplayed = false;
    }

    public void LevelRolledComplete()
    {
        logoAnimator.SetBool("SlideOut", false);
        creditsAnimator.SetBool("SlideOut", true);
    }

    public void CreditsFadedAway()
    {
        logoAnimator.SetBool("SlideOut", true);
        levelsAnimator.SetBool("SlideOut", true);
    }
}
