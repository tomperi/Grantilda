using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoChangeAlpha : MonoBehaviour {

    private Image img;
    private float min = 0.5f;
    private float max = 0.8f;
    private bool isGoingUp;

	// Use this for initialization
	void Start () {
        img = GetComponent<Image>();
        isGoingUp = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isGoingUp)
        {
            if (img.color.a < max)
            {
                Color clr = img.color;
                float a = clr.a;
                clr.a = (a + 0.001f);
                img.color = clr;
            }
            else
            {
                isGoingUp = false;
            }
        }
        else
        {
            if (img.color.a > min)
            {
                Color clr = img.color;
                float a = clr.a;
                clr.a = (a - 0.001f);
                img.color = clr;
            }
            else
            {
                isGoingUp = true;
            }
        }
	}
}
