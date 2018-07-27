using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Dragger : MonoBehaviour {

    private GameController gameController;
    private FrameManager frameManager;
    private Vector3 initialPosition;
    private bool isBeingTouched;
    private Frame frame;
    private float frameSize;
    private GameObject empty;
    private GameObject frames;
    private FrameAttachable frameAttachable;
    private GameObject player;
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = transform.parent;

        if (parentTransform.name.Equals("Rotateable"))
        {
            parentTransform = parentTransform.parent;
        }

        gameController = FindObjectOfType<GameController>();
        frameManager = FindObjectOfType<FrameManager>();
        initialPosition = parentTransform.position;
        isBeingTouched = false;
        frame = parentTransform.GetComponentInChildren<Frame>();
        frameSize = 23.5f;
        frameAttachable = parentTransform.GetComponentInChildren<FrameAttachable>();

        empty = GameObject.Find("Frame" + (frameManager.EmptyFrame.row - 1) + (frameManager.EmptyFrame.col - 1));
        frames = GameObject.Find("Frames");
    }

    private void OnMouseDrag()
    {
        directionFromEmptyFrame dir = findDirectionFromEmptyFrame();
        if (!gameController.IsZoomedIn && frame != null && Input.touchCount == 1 && dir != directionFromEmptyFrame.NotNextToEmptyFrame) 
        {
            isBeingTouched = true;
            frameAttachable.gameObject.SetActive(false);
            Vector3 touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            if (parentTransform.Find("Player") != null)
            {
                player = parentTransform.Find("Player").gameObject;
                if (player != null)
                {
                    player.GetComponent<PlayerController>().StopNavAgent();
                }
            }

            switch (dir)
            {
                case directionFromEmptyFrame.NotNextToEmptyFrame:
                    return;
                    break;
                case directionFromEmptyFrame.AboveEmptyFrame:
                    if (objPosition.z <= initialPosition.z && objPosition.z >= (initialPosition.z - frameSize))
                    {
                        parentTransform.position = new Vector3(transform.position.x, frames.transform.position.y, objPosition.z);
                    }
                    break;
                case directionFromEmptyFrame.BelowEmptyFrame:
                    if (objPosition.z >= initialPosition.z && objPosition.z <= (initialPosition.z + frameSize))
                    {
                        parentTransform.position = new Vector3(transform.position.x, frames.transform.position.y , objPosition.z);
                    }
                    break;
                case directionFromEmptyFrame.LeftOfEmptyFrame:
                    if (objPosition.x >= initialPosition.x && objPosition.x <= (initialPosition.x + frameSize))
                    {
                        parentTransform.position = new Vector3(objPosition.x, frames.transform.position.y, initialPosition.z);
                    }
                    break;
                case directionFromEmptyFrame.RightOfEmptyFrame:
                    if (objPosition.x <= initialPosition.x && objPosition.x >= (initialPosition.x - frameSize))
                    {
                        parentTransform.position = new Vector3(objPosition.x, frames.transform.position.y, initialPosition.z);
                    }
                    break;
                default:
                    break;
            }

        }
        
    }

    

    private void Update()
    {
        if (!gameController.IsZoomedIn && isBeingTouched && Input.touchCount == 0)
        {
            isBeingTouched = false;
            frameAttachable.gameObject.SetActive(true);
            directionFromEmptyFrame direction = findDirectionFromEmptyFrame();
            
            switch (direction)
            {
                case directionFromEmptyFrame.NotNextToEmptyFrame:
                    return;
                    break;
                case directionFromEmptyFrame.AboveEmptyFrame:
                    if (Math.Abs(parentTransform.position.z - initialPosition.z) > (frameSize / 2f))
                    {
                        switchFrames(Direction.Up);
                        return;
                    }
                    break;
                case directionFromEmptyFrame.BelowEmptyFrame:
                    if (Math.Abs(parentTransform.position.z - initialPosition.z) > (frameSize / 2f))
                    {
                        switchFrames(Direction.Down);
                        return;
                    }
                    break;
                case directionFromEmptyFrame.LeftOfEmptyFrame:
                    if (Math.Abs(parentTransform.position.x - initialPosition.x) > (frameSize / 2f))
                    {
                        switchFrames(Direction.Left);
                        return;
                    }
                    break;
                case directionFromEmptyFrame.RightOfEmptyFrame:
                    if (Math.Abs(parentTransform.position.x - initialPosition.x) > (frameSize / 2f))
                    {
                        switchFrames(Direction.Right);
                        return;
                    }
                    break;
                default:
                    break;
            }

            parentTransform.position = initialPosition;
        }
    }

    private void switchFrames(Direction direction)
    {
        Vector3 tempPosition = empty.transform.position;
        frameManager.SwitchFramePositionWithEmptyFramePosition(frame.currentRow, frame.currentCol);
        empty.transform.position = initialPosition;
        initialPosition = parentTransform.position;

        if (parentTransform.Find("Player") != null)
        {
            player = parentTransform.Find("Player").gameObject;
            if (player != null)
            {
                player.GetComponent<PlayerController>().StartNavAgent();
            }
        }
        
    }

    private directionFromEmptyFrame findDirectionFromEmptyFrame()
    {
        int currentRow = frame.currentRow;
        int currentCol = frame.currentCol;
        int emptyRow = frameManager.EmptyFrame.row;
        int emptyCol = frameManager.EmptyFrame.col;

        if (currentRow == emptyRow)
        {
            if (currentCol == emptyCol + 1)
            {
                return directionFromEmptyFrame.RightOfEmptyFrame;
            }
            else if (currentCol == emptyCol - 1)
            {
                return directionFromEmptyFrame.LeftOfEmptyFrame;
            }
        }
        else if (currentCol == emptyCol)
        {
            if (currentRow == emptyRow + 1)
            {
                return directionFromEmptyFrame.BelowEmptyFrame;
            }
            else if (currentRow == emptyRow - 1)
            {
                return directionFromEmptyFrame.AboveEmptyFrame;
            }
        }

        return directionFromEmptyFrame.NotNextToEmptyFrame;
    }


    private enum directionFromEmptyFrame
    {
        NotNextToEmptyFrame,
        AboveEmptyFrame,
        BelowEmptyFrame,
        LeftOfEmptyFrame,
        RightOfEmptyFrame
    }

}
