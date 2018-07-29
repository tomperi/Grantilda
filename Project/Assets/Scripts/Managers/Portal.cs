using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 portalCenter = gameObject.transform.position;
            other.GetComponent<PlayerController>().GoToPosition(portalCenter);
        }
    }
}
