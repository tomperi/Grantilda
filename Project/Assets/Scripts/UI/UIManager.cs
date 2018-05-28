using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool isPause = false;
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public GameObject ConfirmRestart;
    public GameObject ConfirmExit;

    void Start()
    {

        Button[] MenuButtons = PauseMenu.GetComponentsInChildren<Button>();

        foreach(Button button in MenuButtons)
        {
            switch (button.name)
            {
                case "MainMenuButton":
                    button.onClick.AddListener(delegate { DisplayConfirmExit(true); });
                    break;
                case "RestartButton":
                    button.onClick.AddListener(delegate { DisplayConfirmRestart(true); });
                    break;
                case "PlayButton":
                    button.onClick.AddListener(delegate { DisplayPauseMenu(false); });
                    break;
            }
        }

        Button[] ConfirmExitButtons = ConfirmExit.GetComponentsInChildren<Button>();

        foreach(Button button in ConfirmExitButtons)
        {
            switch (button.name)
            {
                case "Back":
                    button.onClick.AddListener(delegate { DisplayConfirmExit(false);});
                    break;
                case "Confirm":
                    button.onClick.AddListener(delegate { GoToMain();});
                    break;
            }
        }

        Button[] ConfirmRestartButtons = ConfirmRestart.GetComponentsInChildren<Button>();

        foreach (Button button in ConfirmRestartButtons)
        {
            switch (button.name)
            {
                case "Back":
                    button.onClick.AddListener(delegate { DisplayConfirmRestart(false);});
                    break;
                case "Confirm":
                    button.onClick.AddListener(delegate { RestartCurrentLevel();});
                    break;
            }
        }

        PauseButton.GetComponent<Button>().onClick.AddListener(delegate { DisplayPauseMenu(true); });
    }

    public void GoToLevel(int id)
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + id;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void DisplayConfirmExit(bool i_Display)
    {
        ConfirmExit.SetActive(i_Display);
        PauseMenu.SetActive(!i_Display);
    }

    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }

    public void DisplayConfirmRestart(bool i_Display)
    {
        ConfirmRestart.SetActive(i_Display);
        PauseMenu.SetActive(!i_Display);
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayPauseMenu(bool i_Display)
    {
        PauseMenu.SetActive(i_Display);
        PauseButton.SetActive(!i_Display);
        isPause = i_Display;
    }

}
