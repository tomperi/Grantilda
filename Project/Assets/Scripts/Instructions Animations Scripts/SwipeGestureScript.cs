using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGestureScript : MonoBehaviour {

    // Use this for initialization
    private const string k_GestureBool = "gestureVisible";
    private Animator gestureAnimator;
    void Start ()
    {
        GameController gameController = FindObjectOfType<GameController>();
        gameController.zoomOutTriggered += playAnimation;
        gameController.zoomInTriggered += stopAnimation;
        gestureAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    { 
		
	}

    private void playAnimation()
    {
        gestureAnimator.SetBool(k_GestureBool, true);
    }

    private void stopAnimation()
    {
        gestureAnimator.SetBool(k_GestureBool, false);
    }
}
