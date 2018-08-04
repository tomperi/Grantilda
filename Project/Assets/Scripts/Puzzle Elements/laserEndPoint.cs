using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEndPoint : MonoBehaviour {

    private Laser laser;
    private bool wasHit;

    private GameObject offSprite;
    private GameObject onSprite;

    private Portal exit;

    public bool WasHit {get {return wasHit;}}

    // Use this for initialization
    void Start () {
        wasHit = false;
        laser = FindObjectOfType<Laser>();

        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        offSprite.SetActive(true);
        onSprite.SetActive(false);

        exit = FindObjectOfType<Portal>();
        if (exit != null)
        {
            exit.TurnOffPortal();
        }
	}
	
	public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            StartCoroutine(activateEndPoint());
        }
    }

    IEnumerator activateEndPoint()
    {
        yield return new WaitForSeconds(1f);
        laser.levelHitTarget();

        offSprite.SetActive(false);
        onSprite.SetActive(true);

        StartCoroutine(waitAndStartAnim());
        StartCoroutine(setExitActive());
    }

    IEnumerator setExitActive()
    {
        laser.resetLaser();

        yield return new WaitForSeconds(1.5f);
        
        if (exit.gameObject.GetComponent<Animator>() != null)
        {
            exit.gameObject.GetComponent<Animator>().SetBool("Open", true);
        }
    }

    IEnumerator waitAndStartAnim()
    {
        yield return new WaitForSeconds(2f);

        if (exit != null)
        {
            exit.TurnOnPortal();
        }

    }
}
