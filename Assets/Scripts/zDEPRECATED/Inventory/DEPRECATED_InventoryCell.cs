using UnityEngine;

[System.Obsolete("This Uses a Deprecated Inventory System, DO NOT USE!")]
public class DEPRECATED_InventoryCell : MonoBehaviour
{
    DEPRECATED_Items item;

    void SetItem(DEPRECATED_Items item)
    {
        this.item = item;
    }
}
