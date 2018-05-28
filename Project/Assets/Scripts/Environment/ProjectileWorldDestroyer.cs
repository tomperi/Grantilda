using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWorldDestroyer : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }
    }
}
