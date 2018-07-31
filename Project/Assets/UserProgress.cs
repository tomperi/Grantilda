using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProgress : MonoBehaviour
{
    public int currentLevelNumber;

	void Start ()
	{
	    int LevelReached = PlayerPrefs.GetInt("LevelReached", 1);
	    if (LevelReached < currentLevelNumber)
	    {
	        PlayerPrefs.SetInt("LevelReached", currentLevelNumber);
        }
    }
}
