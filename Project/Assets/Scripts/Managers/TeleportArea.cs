using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour {

    private void Start()
    {
        gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool portalOpen = GetComponentInParent<Portal>().portalOn;
        if (portalOpen && other.tag == "Player")
        {
            Vector3 portalCenter = gameObject.transform.position;
            other.GetComponent<PlayerController>().GoToPosition(portalCenter);
            other.GetComponentInChildren<SpriteController>().SetTeleportOut();
        }
    }
}
