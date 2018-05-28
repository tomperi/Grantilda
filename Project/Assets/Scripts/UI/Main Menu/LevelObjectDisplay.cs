using UnityEngine;
using UnityEngine.UI;

public class LevelObjectDisplay : MonoBehaviour
{
    public LevelObject levelObject;

    public Image[] stars = new Image[3];
    public Text levelName;

    void Start()
    {
        levelName.text = levelObject.level;

        for (int i = 0; i < 3; i++)
        {
            if (levelObject.stars[i])
            {
                ActivateStar(stars[i]);
            }
            else
            {
                DeactivateStar(stars[i]);
            }
        }
    }

    void ActivateStar(Image star)
    {
        Debug.Log("Active");
        star.color = new Color(1, 1, 1, 1);
    }

    void DeactivateStar(Image star)
    {
        Debug.Log("Not Active");
        star.color = new Color(1, 1, 1, .42f);
        //star.enabled = false;
    }
}