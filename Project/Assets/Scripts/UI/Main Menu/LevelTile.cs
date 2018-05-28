using UnityEngine;
using UnityEngine.UI;

public class LevelTile : MonoBehaviour
{
    public Level m_Level;

    public Text m_LevelName;
    public Image[] m_Stars = new Image[3];

    public void InitTile()
    {
        m_LevelName.text = m_Level.Locked ? "X" : m_Level.LevelName;

        for (int i = 0; i < 3; i++)
        {
            if (m_Level.Stars > i)
            {
                ActivateStar(m_Stars[i]);
            }
            else
            {
                DeactivateStar(m_Stars[i]);
            }
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {
            if (!m_Level.Locked)
            {
                if (m_Level.Completed)
                {
                    // Sure you want to play again?
                }
                else
                {
                    // Add listener
                    button.onClick.AddListener(delegate {ChooseLevel.GoToLevel(m_Level.Id);});
                }
            }
        }
    }

    void ActivateStar(Image star)
    {
        //Debug.Log("Active");
        star.color = new Color(1, 1, 1, 1);
    }

    void DeactivateStar(Image star)
    {
        //Debug.Log("Not Active");
        star.color = new Color(1, 1, 1, .42f);
    }
}

