﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutGesturesScript : MonoBehaviour {

    public bool DisplayGestures { get; set; }
    private const string k_GestureBool = "gestureVisible";
    private Transform gestureObject;

    void Start ()
    {
        DisplayGestures = true;
        FindObjectOfType<GameController>().zoomOutTriggered += disableGestures;

    }

    private void disableGestures()
    {
        DisplayGestures = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (DisplayGestures)
        {
            gestureObject = findGestureObject(other.transform);
            gestureObject.GetComponent<Animator>().SetBool(k_GestureBool, true);
            StartCoroutine(playAnimationForNumberSeconds());
        }
    }

    IEnumerator playAnimationForNumberSeconds()
    {
        yield return new WaitForSeconds(5f);
        gestureObject.GetComponent<Animator>().SetBool(k_GestureBool, false);
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
