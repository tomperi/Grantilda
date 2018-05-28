using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRotate : MonoBehaviour {

    public void EndRotation()
    {
        GetComponent<Animator>().SetBool("Rotate", false);
    }
}
