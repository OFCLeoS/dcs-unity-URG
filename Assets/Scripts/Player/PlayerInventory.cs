using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    InputAction primaryWeaponAction;
    InputAction secondaryWeaponAction;
    InputAction meleeWeaponAction;

    [SerializeField] Transform inventoryParent;

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

    public void ChangePrimaryWeapon(ProjectileWeapon newPrimaryWeapon)
    {
        if (playerAttackController.GetSelectedWeapon() == primaryWeapon)
        {
            playerAttackController.SetSelectedWeapon(newPrimaryWeapon);
        }
        if(primaryWeapon != null) Destroy(primaryWeapon.gameObject);
        newPrimaryWeapon.transform.SetParent(inventoryParent);
        newPrimaryWeapon.transform.localPosition = Vector3.zero; //TODO: SHOW IN ACTUAL CHARACTER POSITION
        primaryWeapon = newPrimaryWeapon;
    }

    public void ChangeSecondaryWeapon(ProjectileWeapon newSecondaryWeapon)
    {
        if (playerAttackController.GetSelectedWeapon() == secondaryWeapon)
        {
            playerAttackController.SetSelectedWeapon(newSecondaryWeapon);
        }
        if(secondaryWeapon != null) Destroy(secondaryWeapon.gameObject);
        newSecondaryWeapon.transform.SetParent(inventoryParent);
        newSecondaryWeapon.transform.localPosition = Vector3.zero; //TODO: SHOW IN ACTUAL CHARACTER POSITION
        secondaryWeapon = newSecondaryWeapon;
    }

    public void ChangeMeleeWeapon(MeleeWeapon newMeleeWeapon)
    {
        if (playerAttackController.GetSelectedWeapon() == meleeWeapon)
        {
            playerAttackController.SetSelectedWeapon(newMeleeWeapon);
        }
        if(meleeWeapon != null) Destroy(meleeWeapon.gameObject);
        newMeleeWeapon.transform.SetParent(inventoryParent);
        newMeleeWeapon.transform.localPosition = Vector3.zero; //TODO: SHOW IN ACTUAL CHARACTER POSITION
        meleeWeapon = newMeleeWeapon;
    }

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
