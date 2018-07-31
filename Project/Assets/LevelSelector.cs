using UnityEngine;

public class LevelSelector : MonoBehaviour
{

    public bool OpenAllLevels;
    public int MaxLevelOpen;
    public GameObject Levels;

	// Use this for initialization
	void Start ()
	{
	    if (OpenAllLevels)
	    {
	        PlayerPrefs.SetInt("LevelReached", 999);
	    }
	    else if (MaxLevelOpen != 0)
	    {
	        PlayerPrefs.SetInt("LevelReached", MaxLevelOpen);
	    }

        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        Debug.Log(levelReached);

	    for (int i = 0; i < Levels.transform.childCount; i++)
	    {
	        LevelEnabled currentLevel = Levels.transform.GetChild(i).gameObject.GetComponent<LevelEnabled>();
	        if (currentLevel != null)
	        {
	            if (i + 1 > levelReached)
	            {
                    // Disable level button
	                currentLevel.DisableLevel();
                    Debug.Log("Disabled " + Levels.transform.GetChild(i).name);
                }
	            else
	            {
                    // Enable level button
                    currentLevel.EnableLevel(i + 1);
                    Debug.Log("Enabled " + Levels.transform.GetChild(i).name);
                }
            }
	    }
	}
}
