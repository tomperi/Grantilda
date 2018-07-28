using UnityEngine;
using UnityEngine.AI;

public class FrameManager : MonoBehaviour
{

    //Initial values that are defined in the heirarchy
    public int rows, cols;
    public int initialPlayerFrameRow;
    public int initialPlayerFrameColumn;
    public int initialEmptyFrameRow;
    public int initialEmptyFrameColumn;

    //Game Objects that need to be defined in the heirarchy
    public GameObject player;

    //Internal values that are used in other files
    private GameObject[,] frames;
    private Position activeFrame;
    private Position emptyFrame;
    private PlayerController playerController;
    private Vector3 playerRelativePosition;

    public Position EmptyFrame { get { return emptyFrame; } }

    void Awake()
    {
        initFrameArray();

        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {

    }

    private void initFrameArray()
    {
        frames = new GameObject[rows + 2, cols + 2];

        for (int i = 1; i <= rows; i++)
        {
            for (int j = 1; j <= cols; j++)
            {
                GameObject currentFrameObject = getFrameGameObject(i,j);
                frames[i, j] = currentFrameObject;
                // Set the position in the frame script
                setFramePosition(currentFrameObject, i, j);
            }
        }

        activeFrame.row = initialPlayerFrameRow + 1;
        activeFrame.col = initialPlayerFrameColumn + 1;
        emptyFrame.row = initialEmptyFrameRow + 1;
        emptyFrame.col = initialEmptyFrameColumn + 1;
    }

    public void SwitchEmptyFrameLocation(int row, int col)
    {
        if (frames[row, col] != null)
        {
            playerRelativePosition = player.transform.parent.transform.position - player.transform.position; 
            SwitchFramePositionWithEmptyFramePosition(row, col);
            SwitchPlayerPosition();
        }
        
    }

    public void SwitchEmptyFrameLocation(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                SwitchEmptyFrameLocation(emptyFrame.row + 1, emptyFrame.col);
                break;
            case Direction.Down:
                SwitchEmptyFrameLocation(emptyFrame.row - 1, emptyFrame.col);
                break;
            case Direction.Left:
                SwitchEmptyFrameLocation(emptyFrame.row, emptyFrame.col + 1);
                break;
            case Direction.Right:
                SwitchEmptyFrameLocation(emptyFrame.row, emptyFrame.col - 1);
                break;
        }
    }

    public void SwitchActiveFrame(int row, int col)
    {
        activeFrame.row = row;
        activeFrame.col = col;
    }

    public void SwitchFramePositionWithEmptyFramePosition(int row, int col)
    {
        GameObject currentEmptyFrame = frames[emptyFrame.row, emptyFrame.col];
        GameObject frameToMove = frames[row, col];

        Vector3 emptyFramePosition = currentEmptyFrame.GetComponent<Transform>().position;
        currentEmptyFrame.GetComponent<Transform>().position = frameToMove.GetComponent<Transform>().position;
        frameToMove.GetComponent<Transform>().position = emptyFramePosition;

        frames[row, col] = currentEmptyFrame;
        frames[emptyFrame.row, emptyFrame.col] = frameToMove;
        setFramePosition(frameToMove, emptyFrame.row, emptyFrame.col);

        emptyFrame.row = row;
        emptyFrame.col = col;
    }

    
    public void SwitchPlayerPosition()
    {
        Transform playerParentFrameTransform = player.transform.parent;
        playerController.StopNavAgent();
        player.gameObject.transform.position = playerParentFrameTransform.position - playerRelativePosition;
        playerController.StartNavAgent();
        playerController.StopAtPlace();
    }
    


    private bool isActiveFrame(int row, int col)
    {
        return row == activeFrame.row && col == activeFrame.col;
    }

    private void setFramePosition(GameObject frame, int row, int col)
    {
        if (frame != null)
        {
            Frame currentFrameScript = frame.GetComponentInChildren<Frame>();
            if (currentFrameScript != null)
            {
                currentFrameScript.currentRow = row;
                currentFrameScript.currentCol = col;
            }
        }
    }

    public GameObject getFrameGameObject(int row, int col)
    {
        return GameObject.Find("Frame" + (row - 1) + (col - 1));
    }
}
