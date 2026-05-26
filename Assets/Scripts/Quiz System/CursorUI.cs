using UnityEngine;
using UnityEngine.InputSystem;

public class CursorUI : MonoBehaviour
{
    [SerializeField] InputActionReference pointerPositionAction;
    private RectTransform _cursorTransfrom;
    private Canvas _parentCanvas;
    private RectTransform _canvasRectTransform;
    private Camera _canvasCamera;

    private void Awake()
    {
        _cursorTransfrom = GetComponent<RectTransform>();
        _parentCanvas = GetComponentInParent<Canvas>();

        if (_parentCanvas != null)
        {
            _canvasRectTransform = _parentCanvas.GetComponent<RectTransform>();
            _canvasCamera = _parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _parentCanvas.worldCamera;
        }

    }

    void OnEnable()
    {   
        Cursor.visible = false;
        pointerPositionAction.action.performed += OnPointerPositionChanged;
    }

    void OnDisable()
    {
        //Cursor.visible = true;
        pointerPositionAction.action.performed -= OnPointerPositionChanged;
    }

    private void OnPointerPositionChanged(InputAction.CallbackContext ctx)
    {
        if(_cursorTransfrom == null || _canvasRectTransform == null) return;

        Vector2 mousePosition = ctx.ReadValue<Vector2>();

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRectTransform, mousePosition, _canvasCamera, out var localPoint))
        {
            _cursorTransfrom.anchoredPosition = localPoint;
        }

    }

}
