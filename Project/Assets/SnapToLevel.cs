using UnityEngine;
using UnityEngine.UI;

public class SnapToLevel : MonoBehaviour {

    public ScrollRect ScrollRect;
    public RectTransform ContentPanel;
    public RectTransform FirstLevel;
    public float jump;

    private bool forceToPosition;
    private Vector2 defaultContentPosition;

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        defaultContentPosition = ScrollRect.transform.InverseTransformPoint(ContentPanel.position);
    }

    public void SnapToFirstLevel()
    {
        // Automatically snap to the first level element. No animation
        Canvas.ForceUpdateCanvases();

        ContentPanel.localPosition = defaultContentPosition;

        animator.SetBool("SlideToFirstLevel", false);

        //Debug.Log("Snap");
    }

    public void SlideToFirstLevel()
    {
        // Slide using animation to the first level
        forceToPosition = true;

        //Debug.Log("Slide to " + defaultContentPosition);
    }

    void Update()
    {
        if (forceToPosition)
        {
            Canvas.ForceUpdateCanvases();

            if (ContentPanel.localPosition.x < defaultContentPosition.x)
            {
                Vector2 newPosition = ContentPanel.localPosition;
                newPosition.x += jump;
                ContentPanel.localPosition = newPosition;
            }
            else
            {
                forceToPosition = false;
                animator.SetBool("SlideToFirstLevel", true);
            }
        }
    }
}
