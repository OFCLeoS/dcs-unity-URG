using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    InputAction primaryWeaponAction;
    InputAction secondaryWeaponAction;
    InputAction meleeWeaponAction;

    [SerializeField] ProjectileWeapon primaryWeapon;
    [SerializeField] ProjectileWeapon secondaryWeapon;
    [SerializeField] MeleeWeapon meleeWeapon;
    // TODO: Special Tactical Slot?

    [SerializeField] PlayerAttackController playerAttackController;

    #region Initialization
    void Awake()
    {
        InitializePlayerInventory();
    }
    void InitializePlayerInventory()
    {
        if (!playerAttackController)
        {
            playerAttackController = GetComponent<PlayerAttackController>();
            if (!playerAttackController)
            {
                Debug.LogError("Player Inventory does not have a \"PlayerAttackController\" assigned to it. It will not work!");
                enabled = false;
                return;
            }
        }
        primaryWeaponAction = InputSystem.actions.FindAction("Primary Weapon");
        secondaryWeaponAction = InputSystem.actions.FindAction("Secondary Weapon");
        meleeWeaponAction = InputSystem.actions.FindAction("Melee Weapon");
    }
    #endregion


    void HandleInventoryAction()
    {
        if (primaryWeaponAction.WasPressedThisFrame() && primaryWeapon != null)
        {
            playerAttackController.SetSelectedWeapon(primaryWeapon);
        }
        else if (secondaryWeaponAction.WasPressedThisFrame() && secondaryWeapon != null)
        {
            playerAttackController.SetSelectedWeapon(secondaryWeapon);
        }
        else if (meleeWeaponAction.WasPressedThisFrame() && meleeWeapon != null)
        {
            playerAttackController.SetSelectedWeapon(meleeWeapon);
        }
    }

    void Update()
    {
        HandleInventoryAction();
    }
}
