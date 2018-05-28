using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAttachable : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile" || other.tag == "Player")
        {
            PlaceObjectUnderParent(other.gameObject);
            //Debug.Log(other.tag + " moved to " + transform.parent.name);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Projectile" && other.transform.parent == null)
        {
            PlaceObjectUnderParent(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Projectile")
        {
            //Debug.Log("bye");
            other.transform.parent = null;
        }
    }

    private void PlaceObjectUnderParent(GameObject i_GameObject)
    {
        i_GameObject.transform.SetParent(this.transform.parent);
    }
}
