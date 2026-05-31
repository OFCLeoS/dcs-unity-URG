using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindowScript : MonoBehaviour, IDragHandler
{

    public Canvas canvas;
    private RectTransform rectTransform;
    private RectTransform canvasRect;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        ClampToCanvas();
    }

    private void ClampToCanvas()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        Vector2 canvasSize = canvasRect.rect.size;
        Vector2 panelSize = rectTransform.rect.size;

        float minX = -(canvasSize.x * 0.5f) + (panelSize.x * rectTransform.pivot.x);
        float maxX =  (canvasSize.x * 0.5f) - (panelSize.x * (1 - rectTransform.pivot.x));
        float minY = -(canvasSize.y * 0.5f) + (panelSize.y * rectTransform.pivot.y);
        float maxY =  (canvasSize.y * 0.5f) - (panelSize.y * (1 - rectTransform.pivot.y));

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        rectTransform.anchoredPosition = pos;
    }
}
