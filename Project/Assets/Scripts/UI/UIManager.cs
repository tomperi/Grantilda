using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void GoToLevel(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            GoToLevel(nextSceneIndex);
        }
        else
        {
            // Show game completed menu, if it doesn't exist, go to the main menu
            LevelUI LevelUI = GameObject.Find("UI Manager").GetComponent<LevelUI>();
            GameObject GameCompletedParent = GameObject.Find("GameCompletedParent");
            GameObject GameCompletedMenu = GameCompletedParent.transform.GetChild(0).gameObject;

            if (LevelUI != null && GameCompletedMenu != null)
            {
                LevelUI.GetComponent<LevelUI>().isPause = true;
                GameCompletedMenu.SetActive(true);
                GameCompletedMenu.GetComponent<Animator>().SetBool("SlideOut", true);
            }
            else
            {
                GoToLevel(0);
            }
        }
    }

    public void GoToPlayStore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.pangolin.grantildaslittletale");
    }
}
