using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoomInOutFlashScript : MonoBehaviour {

    private const string k_ZoomOut = "zoomOut";
    private const string k_ZoomIn = "zoomIn";
    private Animator gestureAnimator;

    // Use this for initialization
    void Start () {
        GameController gameController = FindObjectOfType<GameController>();
        gameController.zoomOutTriggered += zoomOutFlashGesture;
        gameController.zoomInTriggered += zoomInFlashGesture;
        gestureAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void zoomOutFlashGesture()
    {
        gestureAnimator.SetBool(k_ZoomOut, true);
    }

    private void zoomInFlashGesture()
    {
        gestureAnimator.SetBool(k_ZoomIn, true);
    }

    private void makeTransparent()
    {
        gestureAnimator.SetBool(k_ZoomOut, false);
        gestureAnimator.SetBool(k_ZoomIn, false);
    }
}
