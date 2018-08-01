using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public bool isPause = false;

    private UIManager m_UIManager;
    private GameObject PauseButton;
    private GameObject PauseMenu;

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
            }
        }

        Button[] menuButtons = PauseMenu.GetComponentsInChildren<Button>();

        foreach (Button button in menuButtons)
        {
            switch (button.name)
            {
                case "ResumeButton":
                    button.onClick.AddListener(delegate { DisplayPauseMenu(false); });
                    break;
                case "MainMenuButton":
                    button.onClick.AddListener(delegate { m_UIManager.GoToMain(); });
                    break;
            }
        }
        
        PauseButton.GetComponent<Button>().onClick.AddListener(delegate { DisplayPauseMenu(true); });
    }

    public void DisplayPauseMenu(bool i_Display)
    {
        PauseMenu.SetActive(i_Display);
        PauseButton.SetActive(!i_Display);
        isPause = i_Display;
    }
}
