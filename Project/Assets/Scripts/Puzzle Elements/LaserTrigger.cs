using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTrigger : MonoBehaviour {

    private Laser laser;
    private GameController gameController;

    private GameObject offSprite;
    private GameObject onSprite;
    private bool isOff;

    private void Start()
    {
        laser = FindObjectOfType<Laser>();
        gameController = FindObjectOfType<GameController>();

        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        isOff = true;
        offSprite.SetActive(true);
        onSprite.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(waitAndDo());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        laser.resetLaser();  
    }

    IEnumerator waitAndDo()
    {

        if (isOff)
        {
            toggleOn();
        }
        else
        {
            toggleOff();
        }

        yield return new WaitForSeconds(0.3f);

        gameController.zoomInOut();
        laser.ShootLaser();
    }

    private void toggleOn()
    {
        isOff = false;
        offSprite.SetActive(false);
        onSprite.SetActive(true);
    }

    private void toggleOff()
    {
        isOff = true;
        offSprite.SetActive(true);
        onSprite.SetActive(false);
    }

}
