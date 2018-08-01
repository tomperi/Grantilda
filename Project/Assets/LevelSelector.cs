using UnityEngine;

public class LevelSelector : MonoBehaviour
{

    public bool OpenAllLevels;
    public int MaxLevelOpen;
    public GameObject Levels;

    private int numOfClicks;
    private int levelReached;

	// Use this for initialization
	void Start ()
	{
	    numOfClicks = 1;

	    if (OpenAllLevels)
	    {
	        PlayerPrefs.SetInt("LevelReached", 999);
	    }
	    else if (MaxLevelOpen != 0)
	    {
	        PlayerPrefs.SetInt("LevelReached", MaxLevelOpen);
	    }

        initLevels();
	}

    private void initLevels()
    {
        levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            LevelEnabled currentLevel = Levels.transform.GetChild(i).gameObject.GetComponent<LevelEnabled>();
            if (currentLevel != null)
            {
                if (i + 1 > levelReached)
                {
                    // Disable level button
                    currentLevel.DisableLevel();
                    //Debug.Log("Disabled " + Levels.transform.GetChild(i).name);
                }
                else
                {
                    // Enable level button
                    currentLevel.EnableLevel(i + 1);
                    //Debug.Log("Enabled " + Levels.transform.GetChild(i).name);
                }
            }
        }
    }

    public void ResetClick()
    {
        numOfClicks++;
        if (numOfClicks > 7)
        {
            ResetUserProgress();
        }
    }

    private void ResetUserProgress()
    {
        PlayerPrefs.SetInt("LevelReached", 1);
        initLevels();
        Debug.Log("User progress has been reseted");
        numOfClicks = 1;
    }
}
