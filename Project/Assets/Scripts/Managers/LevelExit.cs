using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {

    private MoveToNextLevelScript moveToNextLevelScript;

    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        moveToNextLevelScript = FindObjectOfType<MoveToNextLevelScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            moveToNextLevelScript.OnLevelComplete();
        }
    }
}
