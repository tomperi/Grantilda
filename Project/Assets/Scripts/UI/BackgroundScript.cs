using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour {

    public float speed;
    public float startingXPosition;
    public float endingXPosition;
    public float startingYPosition;
    public float startingZPosition;

    void Start()
    {
        resetPosition();
        GetComponent<Rigidbody>().velocity = transform.right * speed * -1;
    }

    void Update()
    {
        if (this.transform.position.x <= endingXPosition)
        {
            resetPosition();
        }
    }

    private void resetPosition()
    {
        transform.position = new Vector3(startingXPosition, startingYPosition, startingZPosition);
    }
}
