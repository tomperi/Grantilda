using UnityEngine;

public class Frame : MonoBehaviour
{

    public int currentRow;
    public int currentCol;

    private FrameManager frameManager;

    void Start () 
	{
        frameManager = FindObjectOfType<FrameManager>();
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            frameManager.SwitchActiveFrame(currentRow, currentCol);
            //Debug.Log("Current active frame is " + currentRow + ", " + currentCol);
        }
    }
}
