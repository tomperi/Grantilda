using UnityEngine;
using UnityEngine.AI;

public class NewDoor : MonoBehaviour
{
    public bool doorOpen;
    public DoorColor color;

    NavMeshObstacle obstacle;
    private LayerMask openDoorLayerMask;
    private LayerMask closedDoorLayerMask;

    void Start()
    {
        obstacle = GetComponent<NavMeshObstacle>();
        openDoorLayerMask = LayerMask.NameToLayer("OpenDoors");
        closedDoorLayerMask = LayerMask.NameToLayer("Laser");
        setAllChildLayers(closedDoorLayerMask);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" && color == other.GetComponentInParent<NewDoor>().color)
        {
            // Debug.Log("Enter");
            OpenDoors();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door" && color == other.GetComponentInParent<NewDoor>().color)
        {
            // Debug.Log("Exit");
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        doorOpen = true;
        obstacle.enabled = false;
        setAllChildLayers(openDoorLayerMask);
    }

    public void CloseDoors()
    {
        doorOpen = false;
        obstacle.enabled = true;
        setAllChildLayers(closedDoorLayerMask);

    }

    private void setAllChildLayers(int mask)
    {
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            child.gameObject.layer = mask;
        }
    }
}
