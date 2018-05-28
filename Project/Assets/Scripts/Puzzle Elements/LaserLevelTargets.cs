using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLevelTargets : MonoBehaviour {

    private LaserLevelManager manager;
    private bool wasHit = false;

    private GameObject offSprite;
    private GameObject onSprite;

    public bool WasHit { get { return wasHit; } }

    // Use this for initialization
    void Start () {
        manager = FindObjectOfType<LaserLevelManager>();
        wasHit = false;
        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        offSprite.SetActive(true);
        onSprite.SetActive(false);
    }

    public void OnHitTarget()
    {
        if (!wasHit)
        {
            wasHit = true;

            offSprite.SetActive(false);
            onSprite.SetActive(true);

            StartCoroutine(waitAndStartAnim());

            manager.markAsHit();
        }
    }


    IEnumerator waitAndStartAnim()
    {
        yield return new WaitForSeconds(1f);

        if (onSprite.GetComponent<Animator>() != null)
        {
            onSprite.GetComponent<Animator>().Play("ReceiverAnimation");
        }

    }
}
