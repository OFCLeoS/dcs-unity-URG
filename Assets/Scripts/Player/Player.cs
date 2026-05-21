using System;
using UnityEngine;
using UnityEngine.Rendering;

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
    [SerializeField] CrosshairController crosshairController;

    [SerializeField] PlayerInteractionsHandler interactionsHandler;
    [SerializeField] PlayerAttackController attackController;
    [SerializeField] PlayerInventory inventory;

    [SerializeField] StatusEffectController statusEffectController;
    [SerializeField] HumanoidAnimationController animationController;

    [SerializeField] private PlayerUIBehaviour playerUIBehaviour;

    #region Properties
    public PlayerMovement Movement { get { return movement; } }
    public PlayerRotation Rotation { get { return rotation; } }

    public CameraDriver CameraDriver { get { return cameraDriver; } }
    public CrosshairController CrosshairController { get { return crosshairController; } }

    public PlayerInteractionsHandler InteractionsHandler { get { return interactionsHandler; } }
    public PlayerAttackController AttackController { get { return attackController; } }
    public PlayerInventory Inventory { get { return inventory; } }

    public StatusEffectController StatusEffectController { get { return statusEffectController; } }
    #endregion

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        movement = GetComponent<PlayerMovement>();
        rotation = GetComponent<PlayerRotation>();

        crosshairController = GetComponent<CrosshairController>();

        attackController = GetComponent<PlayerAttackController>();
        inventory = GetComponent<PlayerInventory>();

        statusEffectController = GetComponent<StatusEffectController>();
    }
    #endregion

    public void EnableControls()
    {
        movement.enabled = true;
        rotation.enabled = true;
        interactionsHandler.enabled = true;
        attackController.enabled = true;
        inventory.enabled = true;
    }

    public void DisableControls()
    {
        movement.enabled = false;
        rotation.enabled = false;
        interactionsHandler.enabled = false;
        attackController.enabled = false;
        inventory.enabled = false;
    }

    protected override void DestroyEntity()
    {
        DisableControls();
        animationController.PlayerDeathAnimation();
    }

    // TODO: Remove this? Properties now available
    public void ReceiveLoadout(ProjectileWeapon primaryWeapon, ProjectileWeapon secondaryWeapon, MeleeWeapon meleeWeapon)
    {
        if (primaryWeapon != null) inventory.ChangePrimaryWeapon(primaryWeapon);
        if (secondaryWeapon != null) inventory.ChangeSecondaryWeapon(secondaryWeapon);
        if (meleeWeapon != null) inventory.ChangeMeleeWeapon(meleeWeapon);
    }
    void Start()
    {
        playerUIBehaviour.SetMaxHealth(_defaultMaxHealth);
        playerUIBehaviour.SetHealth(currentHealth);  
    }

    public override void TakeDamage(float damageAmount, Team attackingTeam)
    {
        base.TakeDamage(damageAmount, attackingTeam);
        if(currentHealth <= 0)
        {
            playerUIBehaviour.SetHealth(0);  
        }
        else
        {
            playerUIBehaviour.SetHealth(currentHealth);  
        }
    }

    #region DEBUG
#if UNITY_EDITOR

    [ContextMenu("DEBUG_DESTROY")]
    void KILL() => DestroyEntity();

#endif
    #endregion
}