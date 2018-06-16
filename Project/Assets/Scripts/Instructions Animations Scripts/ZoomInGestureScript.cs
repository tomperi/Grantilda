using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInGestureScript : MonoBehaviour {

    // Use this for initialization
    private const string k_GestureBool = "gestureVisible";
    private Animator gestureAnimator;
    private bool playerHasSwiped = false;

    void Start()
    {
        GameController gameController = FindObjectOfType<GameController>();
        gameController.zoomOutTriggered += stopAnimation;
        gameController.zoomInTriggered += stopAnimation;
        gameController.swipeTriggered += firstSwipe;
        gestureAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

    private void firstSwipe()
    {
        if(!playerHasSwiped)
        {
            playAnimation();
            playerHasSwiped = true;
        } 
    }
}

