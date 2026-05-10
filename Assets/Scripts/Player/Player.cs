using System;
using UnityEngine;

/// <summary>
/// Basis of the Player Entity
/// </summary>
[RequireComponent(typeof(PlayerMovement), typeof(PlayerRotation))]
[RequireComponent(typeof(PlayerAttackController), typeof(PlayerInventory))]
public class Player : DamageableEntity
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerRotation rotation;

    [SerializeField] CameraDriver cameraDriver;

    [SerializeField] PlayerInteractionsHandler interactionsHandler;
    [SerializeField] PlayerAttackController attackController;
    [SerializeField] PlayerInventory inventory;

    #region Properties
    public PlayerMovement Movement { get { return movement; } }
    public PlayerRotation Rotation { get { return rotation; } }

    public CameraDriver CameraDriver { get { return cameraDriver; } }

    public PlayerInteractionsHandler InteractionsHandler { get { return interactionsHandler; } }
    public PlayerAttackController AttackController { get { return attackController; } }
    public PlayerInventory Inventory { get { return inventory; } }
    #endregion

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
        rotation = GetComponent<PlayerRotation>();
        interactionsHandler = GetComponent<PlayerInteractionsHandler>();
        attackController = GetComponent<PlayerAttackController>();
        inventory = GetComponent<PlayerInventory>();
    }
    #endregion

    protected override void DestroyEntity()
    {
        movement.enabled = false;
        rotation.enabled = false;

        Debug.Log("Player has died!");
    }
    
    // TODO: Remove this? Properties now available
    public void ReceiveLoadout(ProjectileWeapon primaryWeapon, ProjectileWeapon secondaryWeapon, MeleeWeapon meleeWeapon)
    {
        if (primaryWeapon != null) inventory.ChangePrimaryWeapon(primaryWeapon);
        if (secondaryWeapon != null) inventory.ChangeSecondaryWeapon(secondaryWeapon);
        if (meleeWeapon != null) inventory.ChangeMeleeWeapon(meleeWeapon);
    }

    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void KILL() => DestroyEntity();

#endif
    #endregion
}