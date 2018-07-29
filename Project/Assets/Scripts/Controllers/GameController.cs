using UnityEngine;
using System.Collections;
using ProBuilder2.Common;
using System;

public class GameController : MonoBehaviour
{

    public GameObject zoomOutCamera;
    public Camera mainCamera;
    public PlayerController player;
    public FrameManager frameManager;
    public LayerMask playerLayerMask;
    public LayerMask frameLayerMask;
    public LayerMask floorLayerMask;
    public GameObject pressFeedbackAnimatorObjectPrefab;
    public bool allowZoomInOut;
    public bool startZoomedOut;
    public bool isPlayerInLevel;
    private bool runningOnDesktop;
    public bool RunningOnDesktop {  get { return runningOnDesktop; } }
    private LevelUI levelUI;
    public bool PortalOn;

    private bool isZoomedIn;
    public bool IsZoomedIn { get { return isZoomedIn; } }

    public event Action zoomOutTriggered;
    public event Action zoomInTriggered;
    public event Action swipeTriggered;


    //used for laser levels
    private Laser laser;

    //used for mobile
    private Vector3 firstTouchPosition;
    private Vector3 secondTouchPosition;
    private float dragDistance;
    private float nextZoomOutActionTime;
    private float sensitivityLevel;

    void Awake()
    {
        isZoomedIn = startZoomedOut;
        startZoomInOut();

        laser = FindObjectOfType<Laser>();

        dragDistance = Screen.width * 15 / 100; //drag distance is 15% of the screen
        runningOnDesktop = SystemInfo.deviceType == DeviceType.Desktop;
        levelUI = FindObjectOfType<LevelUI>();
        nextZoomOutActionTime = 0;
        sensitivityLevel = 0.025f;

        if (PortalOn)
        {
            GameObject portal = GameObject.FindGameObjectWithTag("Exit Portal");
            if (portal != null)
            {
                portal.GetComponent<Animator>().SetBool("Open", true);
            }
        }
    }

    void Update()
    {
        if (!isZoomedIn || levelUI.isPause)
        {
            player.StopAtPlace();
        }

        if (!player.GetComponentInChildren<SpriteController>().Teleport)
        {
            //MOBILE

            //zoom in or out Mobile
            if (Input.touchCount == 2 && !levelUI.isPause && Time.realtimeSinceStartup > nextZoomOutActionTime)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                if (deltaMagnitudeDiff < 0 && !isZoomedIn)
                {
                    zoomInOut();
                }
                else if ((deltaMagnitudeDiff > 0 && isZoomedIn))
                {
                    zoomInOut();
                }

                nextZoomOutActionTime = Time.realtimeSinceStartup + sensitivityLevel;
            }

            //move/rotate frame Mobile
            
            if (Input.touchCount == 1 && !isZoomedIn && !levelUI.isPause && Time.realtimeSinceStartup > nextZoomOutActionTime) // user is touching the screen with a single touch
            {
                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    firstTouchPosition = touch.position;
                    secondTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                {
                    secondTouchPosition = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 15% of the screen height
                    if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) > dragDistance || Mathf.Abs(secondTouchPosition.y - firstTouchPosition.y) > dragDistance)
                    {/*
                        //It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) > Mathf.Abs(secondTouchPosition.y - firstTouchPosition.y))
                        {   //If the horizontal movement is greater than the vertical movement...
                            if ((secondTouchPosition.x > firstTouchPosition.x))  //If the movement was to the right)
                            {   //Right swipe
                                frameManager.SwitchEmptyFrameLocation(Direction.Right);
                                StartCoroutine(resetLaser());
                                Debug.Log("Right Swipe");
                                OnSwipeTriggered();
                            }
                            else
                            {   //Left swipe
                                frameManager.SwitchEmptyFrameLocation(Direction.Left);
                                StartCoroutine(resetLaser());
                                Debug.Log("Left Swipe");
                                OnSwipeTriggered();
                            }
                        }
                        else if (Mathf.Abs(secondTouchPosition.x - firstTouchPosition.x) < Mathf.Abs(secondTouchPosition.y - firstTouchPosition.y))
                        {   //the vertical movement is greater than the horizontal movement
                            if (secondTouchPosition.y > firstTouchPosition.y)  //If the movement was up
                            {   //Up swipe
                                frameManager.SwitchEmptyFrameLocation(Direction.Up);
                                StartCoroutine(resetLaser());
                                Debug.Log("Up Swipe");
                                OnSwipeTriggered();

                            }
                            else
                            {   //Down swipe
                                frameManager.SwitchEmptyFrameLocation(Direction.Down);
                                StartCoroutine(resetLaser());
                                Debug.Log("Down Swipe");
                                OnSwipeTriggered();
                            }
                        }*/
                    }
                    else
                    {   //It's a tap as the drag distance is less than 20% of the screen height
                        Ray ray = Camera.main.ScreenPointToRay(touch.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 10000f, floorLayerMask))
                        {
                            GameObject frame = hit.transform.parent.parent.gameObject;

                            if (frame != null)
                            {
                                RotateFrame(frame);
                                //frame.transform.Rotate(new Vector3(0f, 90f, 0f));
                                //Debug.Log("Should rotate " + frame.transform.name);
                                foreach (Transform transformChild in frame.transform) // Messy, needs to fix later! ~ Amir
                                {
                                    if (transformChild.name == "ShadowProjectile(Clone)")
                                    {
                                        transformChild.gameObject.GetComponent<ProjectileController>().ChangeDirectionOnRotate();
                                    }
                                }
                                StartCoroutine(resetLaser());
                            }
                        }
                    }
                }
                nextZoomOutActionTime = Time.realtimeSinceStartup + sensitivityLevel;
            }
            
            //move player mobile
            if (Input.touchCount == 1 && isZoomedIn && !levelUI.isPause)
            {
                Touch touch = Input.touches[0];
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayerMask))
                {
                    GameObject recipient = hit.transform.gameObject;

                    if (touch.phase == TouchPhase.Ended)
                    {
                        player.GoToPosition(hit.point);
                        if (!recipient.tag.Equals("Environment"))
                        {
                            createPressFeedbackAnimation(hit.point);
                        }
                    }
                }
            }

            //PC
            {
                //zoom in or out PC
                if (runningOnDesktop && Input.GetButtonDown("Jump"))
                {
                    zoomInOut();
                }

                //move player PC
                if (runningOnDesktop && Input.GetMouseButtonDown(0))
                {
                    if (isZoomedIn && isPlayerInLevel)
                    {
                        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, 10000f, playerLayerMask))
                        {
                            GameObject recipient = hit.transform.gameObject;

                            player.GoToPosition(hit.point);
                            if (!recipient.tag.Equals("Environment"))
                            {
                                createPressFeedbackAnimation(hit.point);
                            }
                        }
                    }
                    else
                    {
                        // Use raycast to change frames
                    }
                }

                //rotate frame PC
                if (runningOnDesktop && Input.GetMouseButtonDown(1)) // Mouse Right Click
                {
                    if (!isZoomedIn)
                    {
                        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;


                        if (Physics.Raycast(ray, out hit, 10000f, floorLayerMask))
                        {
                            GameObject frame = hit.transform.parent.parent.gameObject;

                            if (frame != null)
                            {
                                RotateFrame(frame);
                                //frame.transform.Rotate(new Vector3(0f, 90f, 0f));
                                //Debug.Log("Should rotate " + frame.transform.name);

                                /*
                                foreach (Transform transformChild in frame.transform) // Messy, needs to fix later! ~ Amir
                                {
                                    if (transformChild.name == "ShadowProjectile(Clone)")
                                    {
                                        transformChild.gameObject.GetComponent<ProjectileController>().ChangeDirectionOnRotate();
                                    }
                                }
                                */
                                StartCoroutine(resetLaser());
                            }
                        }
                    }
                }
                if (runningOnDesktop)
                {
                    // Move frame PC
                    if ((Input.GetKeyDown(KeyCode.UpArrow)) && (!isZoomedIn))
                    {
                        frameManager.SwitchEmptyFrameLocation(Direction.Up);
                        StartCoroutine(resetLaser());
                        OnSwipeTriggered();
                    }

                    if ((Input.GetKeyDown(KeyCode.RightArrow)) && (!isZoomedIn))
                    {
                        frameManager.SwitchEmptyFrameLocation(Direction.Right);
                        StartCoroutine(resetLaser());
                        OnSwipeTriggered();
                    }

                    if ((Input.GetKeyDown(KeyCode.DownArrow)) && (!isZoomedIn))
                    {
                        frameManager.SwitchEmptyFrameLocation(Direction.Down);
                        StartCoroutine(resetLaser());
                        OnSwipeTriggered();
                    }

                    if ((Input.GetKeyDown(KeyCode.LeftArrow)) && (!isZoomedIn))
                    {
                        frameManager.SwitchEmptyFrameLocation(Direction.Left);
                        StartCoroutine(resetLaser());
                        OnSwipeTriggered();
                    }
                }
            }

            if (runningOnDesktop)
            {
                // pause PC (not replicated in mobile)
                if ((Input.GetKeyDown(KeyCode.P)))
                {
                    ToggleTimeScale();
                }

                //pause game
                if (Input.GetKeyUp(KeyCode.Escape))
                {
                    levelUI.DisplayPauseMenu(!levelUI.isPause);
                }
            }
        }
    }

    private void pauseGame()
    {
        throw new NotImplementedException();
    }

    private void RotateFrame(GameObject i_Frame)
    {
        Animator[] animator = i_Frame.GetComponentsInChildren<Animator>();
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayRotateFrameSoundEffect();
        }
        

        foreach (Animator anim in animator)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("FrameIdle") || anim.GetCurrentAnimatorStateInfo(0).IsName("UnderIdle"))
            {
                anim.SetBool("Rotate", true);
            }
        }

        // Check if the player is a child of i_Frame, if so, call the SpriteController.StartShrinkPlayer
        SpriteController playerSprite = i_Frame.GetComponentInChildren<SpriteController>();
        if (playerSprite != null)
        {
            playerSprite.StartShrinkPlayer();
        }
    }

    public void zoomInOut()
    {
        if (allowZoomInOut)
        {
            if (isZoomedIn)
            {
                zoomOutCamera.SetActive(true);
                isZoomedIn = false;
                if (isPlayerInLevel)
                {
                    player.StopAtPlace();
                }
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.ToggleZoomSoundAction(false);
                }
                //Debug.Log("Zoom out");
                OnZoomOut();
            }
            else
            {
                zoomOutCamera.SetActive(false);
                isZoomedIn = true;
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.ToggleZoomSoundAction(true);
                }
                //Debug.Log("Zoom in");
                OnZoomIn();
            }
        }
    }

    public void startZoomInOut()
    {
        if (isZoomedIn)
        {
            zoomOutCamera.SetActive(true);
            isZoomedIn = false;
            if (isPlayerInLevel)
            {
                player.StopAtPlace();
            }
            //Debug.Log("Zoom out");
        }
        else
        {
            zoomOutCamera.SetActive(false);
            isZoomedIn = true;
            //Debug.Log("Zoom in");
        }
    }

    protected virtual void OnZoomOut()
    {
        if (zoomOutTriggered != null)
        {
            zoomOutTriggered.Invoke();
        }
    }

    protected virtual void OnZoomIn()
    {
        if (zoomInTriggered != null)
        {
            zoomInTriggered.Invoke();
        }
    }

    protected virtual void OnSwipeTriggered()
    {
        if (swipeTriggered != null)
        {
            swipeTriggered.Invoke();
        }
    }


    IEnumerator resetLaser()
    {
        if (laser != null)
        {
            laser.resetLaser();
        }

        yield return new WaitForSeconds(0.1f);
    }

    private void ToggleTimeScale()
    {
        Time.timeScale = (Time.timeScale + 1) % 2;
    }

    private void createPressFeedbackAnimation(Vector3 pressLocation)
    {
        GameObject pressFeedbackAnimatorObject = Instantiate(pressFeedbackAnimatorObjectPrefab);
        pressFeedbackAnimatorObject.transform.position = pressLocation;
    }
}
