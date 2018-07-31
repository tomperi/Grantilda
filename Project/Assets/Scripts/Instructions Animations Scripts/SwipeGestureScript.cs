using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeGestureScript : MonoBehaviour {

    // Use this for initialization
    private const string k_GestureBool = "gestureVisible";
    private Animator gestureAnimator;
    private bool playerHasSwiped = false;
    private bool zoomOutActivated = false;
    private GameController gameController;
    public GameObject triggeringDragger;

    void Start ()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.zoomOutTriggered += playAnimation;
        gameController.zoomInTriggered += stopAnimation;
        gameController.swipeTriggered += firstSwipe;
        Dragger dragger = triggeringDragger.GetComponent<Dragger>();
        dragger.dragTriggered += firstSwipe;
        gestureAnimator = GetComponent<Animator>();
        
    }

    IEnumerator playAnimationForNumberSeconds(float seconds)
    {     
        yield return new WaitForSeconds(seconds);
        if (!playerHasSwiped && zoomOutActivated)
        {
            gestureAnimator.SetBool(k_GestureBool, true);
        }
    }

    // Update is called once per frame
    void Update ()
    { 
		
	}

    private void playAnimation()
    {
        if (!playerHasSwiped)
        {
            zoomOutActivated = true;
            gameController.allowZoomInOut = false;
            StartCoroutine(playAnimationForNumberSeconds(1.5f));
        }
    }

    private void stopAnimation()
    {
        zoomOutActivated = false;
        gestureAnimator.SetBool(k_GestureBool, false);
        gameController.allowZoomInOut = true;
    }

    private void firstSwipe()
    {
        playerHasSwiped = true;
        stopAnimation();
    }
}
