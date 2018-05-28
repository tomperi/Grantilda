using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelManager : MonoBehaviour {

    private bool allWereHit;
    private bool[] targetsHit;
    private LevelExit exit;

    // Use this for initialization
    void Start () {
        allWereHit = false;
        targetsHit = new bool[3];
        targetsHit[0] = false;
        targetsHit[1] = false;
        targetsHit[2] = false;

        exit = FindObjectOfType<LevelExit>();
        exit.gameObject.SetActive(false);
    }
	public bool wereAllHit()
    {
        return allWereHit;
    }

    public void markAsHit()
    {
        if (!allWereHit)
        {
            if (!targetsHit[0])
            {
                targetsHit[0] = true;
            }
            else if (!targetsHit[1])
            {
                targetsHit[1] = true;
            }
            else if (!targetsHit[2])
            {
                targetsHit[2] = true;
                allWereHit = true;
                StartCoroutine(activateExit());
            }
        }
    }

    IEnumerator activateExit()
    {
        yield return new WaitForSeconds(1f);
        exit.gameObject.SetActive(true);
    }
}
