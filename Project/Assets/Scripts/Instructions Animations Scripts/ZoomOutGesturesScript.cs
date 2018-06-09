using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutGesturesScript : MonoBehaviour {

    public GameObject zoomOutGestureObject;
    private bool wasTriggered;

	// Use this for initialization
	void Start () {
        wasTriggered = false;
        zoomOutGestureObject.SetActive(false);
	}

    public void OnTriggerEnter(Collider other)
    {
        if (!wasTriggered)
        {
            wasTriggered = true;
            zoomOutGestureObject.SetActive(true);
            StartCoroutine(playAnimationForNumberSeconds());
        }
    }

    IEnumerator playAnimationForNumberSeconds()
    {
        yield return new WaitForSeconds(5f);

        zoomOutGestureObject.SetActive(false);
    }
}
