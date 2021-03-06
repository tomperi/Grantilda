﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutGestureScript : MonoBehaviour {

    public bool DisplayGestures { get; set; }
    private const string k_GestureBool = "gestureVisible";
    private Transform gestureObject;
    private GameController gameController;

    void Start ()
    {
        DisplayGestures = true;
        FindObjectOfType<GameController>().zoomOutTriggered += disableGestures;
        gameController = FindObjectOfType<GameController>();
        gameController.allowZoomInOut = false;
    }

    private void disableGestures()
    {
        DisplayGestures = false;
        if (gestureObject != null && gestureObject.gameObject.activeInHierarchy)
        {
            Animator gestureAnimator = gestureObject.GetComponent<Animator>();
            gestureAnimator.SetBool(k_GestureBool, false);
            gestureObject.gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (DisplayGestures)
        {
            gestureObject = findGestureObject(other.transform);
            StartCoroutine(playAnimationForNumberSeconds(5f));
        }
    }

    IEnumerator playAnimationForNumberSeconds(float seconds)
    {
        Animator gestureAnimator = gestureObject.GetComponent<Animator>();
        gestureAnimator.SetBool(k_GestureBool, true);
        gameController.allowZoomInOut = true;
        yield return new WaitForSeconds(seconds);
        if (gestureObject.gameObject.activeInHierarchy)
        {
            gestureAnimator.SetBool(k_GestureBool, false);
        }
    }

    private Transform findGestureObject(Transform gameObjectTransform)
    {
        return findObjectWithTag(gameObjectTransform, "Gesture");
    }

    private Transform findObjectWithTag(Transform gameObjectTransform, string tag)
    {
        return getChildObject(gameObjectTransform, tag);
    }

    private Transform getChildObject(Transform gameObjectTransform , string tag)
    {
        Transform child = null;
        for (int i = 0; i < gameObjectTransform.childCount; i++)
        {
            child = gameObjectTransform.GetChild(i);
            if (child.tag == tag)
            {
                break;
            }
        }

        return child;
    }
}
