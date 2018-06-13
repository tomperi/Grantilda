using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            GoToLevel(0);
        }
    }
}
