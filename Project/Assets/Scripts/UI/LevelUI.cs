using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public bool isPause = false;

    private UIManager m_UIManager;
    private GameObject PauseButton;
    private GameObject PauseMenu;
    private GameObject ConfirmRestart;
    private GameObject ConfirmExit;

    void Start()
    {
        // Init all objects
        m_UIManager = FindObjectOfType<UIManager>();
        GameObject levelUI = GameObject.Find("LevelUI");
        for (int i = 0; i < levelUI.transform.childCount; i++)
        {
            GameObject child = levelUI.transform.GetChild(i).gameObject;
            switch(child.name)
            {
                case ("PauseButton"):
                    PauseButton = child;
                    break;
                case ("PauseMenu"):
                    PauseMenu = child;
                    break;
                case ("Confirm Restart"):
                    ConfirmRestart = child;
                    break;
                case ("Confirm Exit"):
                    ConfirmExit = child;
                    break;
            }
        }

        Button[] menuButtons = PauseMenu.GetComponentsInChildren<Button>();

        foreach (Button button in menuButtons)
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

        Button[] confirmExitButtons = ConfirmExit.GetComponentsInChildren<Button>();

        foreach (Button button in confirmExitButtons)
        {
            switch (button.name)
            {
                case "Back":
                    button.onClick.AddListener(delegate { DisplayConfirmExit(false); });
                    break;
                case "Confirm":
                    button.onClick.AddListener(delegate { m_UIManager.GoToMain(); });
                    break;
            }
        }

        Button[] confirmRestartButtons = ConfirmRestart.GetComponentsInChildren<Button>();

        foreach (Button button in confirmRestartButtons)
        {
            switch (button.name)
            {
                case "Back":
                    button.onClick.AddListener(delegate { DisplayConfirmRestart(false); });
                    break;
                case "Confirm":
                    button.onClick.AddListener(delegate { m_UIManager.RestartCurrentLevel(); });
                    break;
            }
        }

        PauseButton.GetComponent<Button>().onClick.AddListener(delegate { DisplayPauseMenu(true); });
    }

    public void DisplayConfirmExit(bool i_Display)
    {
        ConfirmExit.SetActive(i_Display);
        PauseMenu.SetActive(!i_Display);
    }

    public void DisplayConfirmRestart(bool i_Display)
    {
        ConfirmRestart.SetActive(i_Display);
        PauseMenu.SetActive(!i_Display);
    }

    public void DisplayPauseMenu(bool i_Display)
    {
        PauseMenu.SetActive(i_Display);
        PauseButton.SetActive(!i_Display);
        isPause = i_Display;
    }
}
