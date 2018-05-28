using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserEndPoint : MonoBehaviour {

    private Laser laser;
    private bool wasHit;

    private GameObject offSprite;
    private GameObject onSprite;

    private LevelExit exit;

    public bool WasHit {get {return wasHit;}}

    // Use this for initialization
    void Start () {
        wasHit = false;
        laser = FindObjectOfType<Laser>();

        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        offSprite.SetActive(true);
        onSprite.SetActive(false);

        exit = FindObjectOfType<LevelExit>();
        exit.gameObject.SetActive(false);
	}
	
	public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;
            laser.levelHitTarget();

            offSprite.SetActive(false);
            onSprite.SetActive(true);

            StartCoroutine(waitAndStartAnim());
            StartCoroutine(setExitActive());
        }
    }

    IEnumerator setExitActive()
    {
        yield return new WaitForSeconds(3f);
        exit.gameObject.SetActive(true);
    }

    IEnumerator waitAndStartAnim()
    {
        yield return new WaitForSeconds(2f);

        if (onSprite.GetComponent<Animator>() != null)
        {
            onSprite.GetComponent<Animator>().Play("ReceiverAnimation");
        }

    }
}
