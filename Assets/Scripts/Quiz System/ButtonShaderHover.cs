using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShaderHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Material buttonMat;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().material = buttonMat;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().material = null;
    }

}
