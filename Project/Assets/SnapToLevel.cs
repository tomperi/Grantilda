using UnityEngine;
using UnityEngine.UI;

public class SnapToLevel : MonoBehaviour {

    public ScrollRect ScrollRect;
    public RectTransform ContentPanel;
    public RectTransform FirstLevel;

    private bool forceToPosition;
    private Vector2 position;

    public void SnapToFirstLevel()
    {
        //forceToPosition = true;
        //position = (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
        //           - (Vector2)ScrollRect.transform.InverseTransformPoint(FirstLevel.position);

        // Automatically snap to the first level element. No animation
        Canvas.ForceUpdateCanvases();

        ContentPanel.localPosition =
            (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
            - (Vector2)ScrollRect.transform.InverseTransformPoint(FirstLevel.position);

        Debug.Log("Snap to " + FirstLevel);
    }

    public void SlideToFirstLevel()
    {
        // Slide using animation to the first level
        forceToPosition = true;
        position = (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
                   - (Vector2)ScrollRect.transform.InverseTransformPoint(FirstLevel.position);
        
        //Canvas.ForceUpdateCanvases();

        //ContentPanel.localPosition =
        //    (Vector2)ScrollRect.transform.InverseTransformPoint(ContentPanel.position)
        //    - (Vector2)ScrollRect.transform.InverseTransformPoint(EmptyTransform.position);

        Debug.Log("Slide to " + FirstLevel);
    }

    void Update()
    {
        if (forceToPosition)
        {
            Canvas.ForceUpdateCanvases();

            if (ContentPanel.localPosition.x > position.x)
            {
                Vector2 newPosition = ContentPanel.localPosition;
                newPosition.x += 50f;
                ContentPanel.localPosition = newPosition;
            }
            else
            {
                forceToPosition = false;
            }
        }
    }
}
