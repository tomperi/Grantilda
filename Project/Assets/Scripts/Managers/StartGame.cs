using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour {

    private MoveToNextLevelScript moveToNextLevelScript;

	// Use this for initialization
	void Start () {
        moveToNextLevelScript = FindObjectOfType<MoveToNextLevelScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveToNextLevelScript.OnLevelComplete();
        }

        if (Input.touches.Length > 0)
        {
            moveToNextLevelScript.OnLevelComplete();
        }
    }

    
}
