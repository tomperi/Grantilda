using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("ClosedDoors") || other.gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Floating Stone")
        {
            gameObject.GetComponent<ProjectileController>().projectileManager.DestroyFloatingStone();
            Destroy(gameObject);
        }
    }



}
