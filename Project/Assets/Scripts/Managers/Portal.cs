using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool portalOn;
    private Animator animator;

    private void Awake()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        animator = GetComponent<Animator>();
    }

    public void TurnOnPortal()
    {
        portalOn = true;
        animator.SetBool("Open", true);
    }

    public void TurnOffPortal()
    {
        portalOn = false;
        animator.SetBool("Open", false);
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
