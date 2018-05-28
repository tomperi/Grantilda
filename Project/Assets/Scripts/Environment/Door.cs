using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{

    public GameObject door;
    public DoorColor color;
    public bool[] colliders = new bool[2];
    private LayerMask openDoorLayerMask;
    private LayerMask closedDoorLayerMask;

    Animator animator;
    NavMeshObstacle obstacle;
    public bool doorOpen;

    void Start()
    {
        animator = door.GetComponent<Animator>();
        obstacle = door.GetComponent<NavMeshObstacle>();

        openDoorLayerMask = LayerMask.NameToLayer("OpenDoors");
        closedDoorLayerMask = LayerMask.NameToLayer("Laser");
        setAllChildLayers(closedDoorLayerMask);
        
    }

    void Update()
    {
        if (colliders[0] && colliders[1])
        {
            if (!doorOpen)
            {
                OpenDoors();
            }
        }
        else if (doorOpen)
        {
            CloseDoors();
        }

        if (doorOpen)
        {
//            makeSureDoorIsOpen();
        }
        else
        {
//            makeSureDoorIsClose();
        }
    }

    private void makeSureDoorIsClose()
    {
        Color currentColor = door.GetComponent<MeshRenderer>().material.color;
        if ((currentColor.a >= 0) && (currentColor.a <= 1))
        {
            currentColor.a -= 0.02f;
            door.GetComponent<MeshRenderer>().materials.First().color = currentColor;
        }
    }

    private void makeSureDoorIsOpen()
    {
        Color currentColor = door.GetComponent<MeshRenderer>().material.color;
        if ((currentColor.a >= 0) && (currentColor.a <= 1))
        {
            currentColor.a += 0.02f;
            door.GetComponent<MeshRenderer>().materials.First().color = currentColor;
        }
    }

    public void OpenDoors()
    {
        doorOpen = true;
        obstacle.enabled = false;
        //        door.GetComponent<MeshRenderer>().enabled = false;
        animator.ResetTrigger("Close");
        animator.SetTrigger("Open");
        setAllChildLayers(openDoorLayerMask);
    }

    public void CloseDoors()
    {
        doorOpen = false;
        obstacle.enabled = true;
        //        door.GetComponent<MeshRenderer>().enabled = true;
        animator.ResetTrigger("Open");
        animator.SetTrigger("Close");
        setAllChildLayers(closedDoorLayerMask);
    }

    void Doors(string direction)
    {
        animator.SetTrigger(direction);
        Debug.Log(this.gameObject.name + " " + direction);
    }

    private void setAllChildLayers(int mask)
    {
        gameObject.layer = mask;

        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.layer = mask;
        }

    }
}
