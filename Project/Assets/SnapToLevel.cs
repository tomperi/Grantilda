using UnityEngine;
using UnityEngine.UI;

public class SnapToLevel : MonoBehaviour {

    public ScrollRect ScrollRect;
    public RectTransform ContentPanel;
    public RectTransform FirstLevel;
    public float jump;

    private bool forceToPosition;
    private Vector2 position;

    private Animator animator;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void SnapToFirstLevel()
    {
        // Automatically snap to the first level element. No animation
        Canvas.ForceUpdateCanvases();

        ContentPanel.localPosition =
            (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
            - (Vector2)ScrollRect.transform.InverseTransformPoint(FirstLevel.position);

        animator.SetBool("SlideToFirstLevel", false);
    }

    public void SlideToFirstLevel()
    {
        // Slide using animation to the first level
        forceToPosition = true;
        position = (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
                   - (Vector2)ScrollRect.transform.InverseTransformPoint(FirstLevel.position);
    }

    void Update()
    {
        if (forceToPosition)
        {
            Canvas.ForceUpdateCanvases();

            if (ContentPanel.localPosition.x < position.x)
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
