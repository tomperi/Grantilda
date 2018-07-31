using UnityEngine;
using UnityEngine.UI;

public class LevelEnabled : MonoBehaviour
{
    private Image image;
    private Button button;

	void Awake()
	{
	    image = GetComponent<Image>();
	    button = GetComponent<Button>();
	}

    public void EnableLevel(int i_Level)
    {
        button.onClick.AddListener(delegate { ChooseLevel.GoToLevel(i_Level); });
    }

    public void DisableLevel()
    {
        image.color = new Color(1, 1, 1, .33f);
        button.enabled = false;
    }
}
