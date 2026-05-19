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

    [SerializeField] Transform primaryWeaponSlot;
    [SerializeField] Transform secondaryWeaponSlot;
    [SerializeField] Transform meleeWeaponSlot;
    // TODO: Special Tactical Slot?

    [SerializeField] PlayerAttackController playerAttackController;
    [SerializeField] HumanoidPlayerAnimationController playerAnimationController;

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
        if (playerAttackController.GetSelectedWeapon() != null && playerAttackController.GetSelectedWeapon() == primaryWeapon)
        {
            playerAttackController.SetSelectedWeapon(newPrimaryWeapon);
        }
        if (primaryWeapon != null) Destroy(primaryWeapon.gameObject);
        newPrimaryWeapon.transform.SetParent(primaryWeaponSlot);
        newPrimaryWeapon.transform.localPosition = newPrimaryWeapon.PositionInHolster;
        newPrimaryWeapon.transform.localRotation = Quaternion.Euler(newPrimaryWeapon.RotationInHolster);
        primaryWeapon = newPrimaryWeapon;
    }

    public void ChangeSecondaryWeapon(ProjectileWeapon newSecondaryWeapon)
    {
        if (playerAttackController.GetSelectedWeapon() != null && playerAttackController.GetSelectedWeapon() == secondaryWeapon)
        {
            playerAttackController.SetSelectedWeapon(newSecondaryWeapon);
        }
        if (secondaryWeapon != null) Destroy(secondaryWeapon.gameObject);
        newSecondaryWeapon.transform.SetParent(secondaryWeaponSlot);
        newSecondaryWeapon.transform.localPosition = newSecondaryWeapon.PositionInHolster;
        newSecondaryWeapon.transform.localRotation = Quaternion.Euler(newSecondaryWeapon.RotationInHolster);
        secondaryWeapon = newSecondaryWeapon;
    }

    public void ChangeMeleeWeapon(MeleeWeapon newMeleeWeapon)
    {
        if (playerAttackController.GetSelectedWeapon() != null && playerAttackController.GetSelectedWeapon() == meleeWeapon)
        {
            playerAttackController.SetSelectedWeapon(newMeleeWeapon);
        }
        if (meleeWeapon != null) Destroy(meleeWeapon.gameObject);
        newMeleeWeapon.transform.SetParent(meleeWeaponSlot);
        newMeleeWeapon.transform.localPosition = newMeleeWeapon.PositionInHolster;
        newMeleeWeapon.transform.localRotation = Quaternion.Euler(newMeleeWeapon.RotationInHolster);
        meleeWeapon = newMeleeWeapon;
    }


    void HolsterEquippedWeapon()
    {
        if (playerAttackController.GetSelectedWeapon() == primaryWeapon)
        {
            primaryWeapon.transform.SetParent(primaryWeaponSlot);
            primaryWeapon.transform.localPosition = primaryWeapon.PositionInHolster;
            primaryWeapon.transform.localRotation = Quaternion.Euler(primaryWeapon.RotationInHolster);
        }
        else if (playerAttackController.GetSelectedWeapon() == secondaryWeapon)
        {
            secondaryWeapon.transform.SetParent(secondaryWeaponSlot);
            secondaryWeapon.transform.localPosition = secondaryWeapon.PositionInHolster;
            secondaryWeapon.transform.localRotation = Quaternion.Euler(secondaryWeapon.RotationInHolster);
        }
        else if (playerAttackController.GetSelectedWeapon() == meleeWeapon)
        {
            meleeWeapon.transform.SetParent(meleeWeaponSlot);
            meleeWeapon.transform.localPosition = meleeWeapon.PositionInHolster;
            meleeWeapon.transform.localRotation = Quaternion.Euler(meleeWeapon.RotationInHolster);
        }
    }

    void HandleInventoryAction()
    {
        if (primaryWeaponAction.WasPressedThisFrame() && primaryWeapon != null && playerAttackController.GetSelectedWeapon() != primaryWeapon)
        {
            HolsterEquippedWeapon();
            playerAnimationController.ChangeAnimatorController(primaryWeapon.AnimatorController);
            playerAttackController.SetSelectedWeapon(primaryWeapon);
        }
        else if (secondaryWeaponAction.WasPressedThisFrame() && secondaryWeapon != null && playerAttackController.GetSelectedWeapon() != secondaryWeapon)
        {
            HolsterEquippedWeapon();
            playerAnimationController.ChangeAnimatorController(secondaryWeapon.AnimatorController);
            playerAttackController.SetSelectedWeapon(secondaryWeapon);
        }
        else if (meleeWeaponAction.WasPressedThisFrame() && meleeWeapon != null && playerAttackController.GetSelectedWeapon() != meleeWeapon)
        {
            HolsterEquippedWeapon();
            playerAnimationController.ChangeAnimatorController(meleeWeapon.AnimatorController);
            playerAttackController.SetSelectedWeapon(meleeWeapon);
        }
    }

    void Update()
    {
        HandleInventoryAction();
    }
}
