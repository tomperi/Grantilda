using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTextScript : MonoBehaviour {

    private string initialString;
    private string triggeredString;
    public Text levelText;

    private bool wasTriggered;

	// Use this for initialization
	void Start () {
        initialString = "Grandma can only move between tiles with open doors";
        triggeredString = "Press `space` to change to the Tile View" + System.Environment.NewLine + "Move tiles with the arrow keys" + System.Environment.NewLine + "Then press on `space` again to return to Grandma";

        levelText.text = initialString;
        wasTriggered = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!wasTriggered)
        {
            levelText.text = triggeredString;
            wasTriggered = true;
        }
    }
}
