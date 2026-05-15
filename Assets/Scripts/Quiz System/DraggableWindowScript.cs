using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindowScript : MonoBehaviour, IDragHandler
{

    public Canvas canvas;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
