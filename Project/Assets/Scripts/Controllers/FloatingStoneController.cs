using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStoneController : MonoBehaviour {

    private Animator animator;
    private Portal exit;
    private GameController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = FindObjectOfType<GameController>();
        exit = FindObjectOfType<Portal>();
        exit.TurnOffPortal();
    }

    public void PlayDeathAndDestory()
    {
        animator.SetBool("StoneDeath", true);
        StartCoroutine(CompleteLevelCoroutine());
        //Destroy(gameObject, 2f);
    }

    IEnumerator CompleteLevelCoroutine()
    {
        if (controller.IsZoomedIn)
        {
            controller.zoomInOut();
        }
        yield return new WaitForSeconds(2f);
        exit.TurnOnPortal();
        gameObject.SetActive(false);
    }



}
