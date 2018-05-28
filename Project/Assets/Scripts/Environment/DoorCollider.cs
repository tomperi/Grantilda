using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour
{

    public Door parent;
    public int colliderNumber;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Door" && other.transform.parent.gameObject.GetComponent<Door>().color == parent.color)
        {
            parent.colliders[colliderNumber] = true;
        }
        // Debug.Log("Enter " + colliderNumber + " " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Door" && other.transform.parent.gameObject.GetComponent<Door>().color == parent.color)
        {
            parent.colliders[colliderNumber] = false;
        }
        // Debug.Log("Exit " + colliderNumber + " " + other.name);
    }

}
