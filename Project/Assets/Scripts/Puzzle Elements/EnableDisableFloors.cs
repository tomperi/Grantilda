using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisableFloors : MonoBehaviour {

    public GameObject[] gameObjects;
    private GameController gameController;

    private GameObject offSprite;
    private GameObject onSprite;
    private bool isOff;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();

        offSprite = transform.GetChild(0).gameObject;
        onSprite = transform.GetChild(1).gameObject;

        isOff = true;
        offSprite.SetActive(true);
        onSprite.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(zoomOutAndSwitch());   
        }
    }

    IEnumerator zoomOutAndSwitch()
    {
        if (isOff)
        {
            isOff = false;
            offSprite.SetActive(false);
            onSprite.SetActive(true);
        }
        else
        {
            isOff = true;
            offSprite.SetActive(true);
            onSprite.SetActive(false);
        }

        yield return new WaitForSeconds(0.3f);

        if (gameController.IsZoomedIn)
        {
            gameController.zoomInOut();
        }

        yield return new WaitForSeconds(1f);

        foreach (GameObject go in gameObjects)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject child = go.transform.GetChild(i).gameObject;
                child.SetActive(!child.activeSelf);
            }
        }
    }
}
