using UnityEngine;
using UnityEngine.UI;

public class HotbarSlot : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image weaponIcon;

    private bool isSelected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public bool GetIsSelected()
    {
        return isSelected;
    }

    public void SetSelected(bool selected, Sprite selectedImage, Sprite defaultImage)
    {
        isSelected = selected;
        background.sprite = selected ? selectedImage : defaultImage;
    }

    public void SetIcon(Sprite icon)
    {
        weaponIcon.sprite = icon;
        if (icon != null)
        {
            weaponIcon.enabled = true;
        }
        else
        {
            weaponIcon.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
