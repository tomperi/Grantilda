using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public void GoToLevel(int id)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + id;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }


}
