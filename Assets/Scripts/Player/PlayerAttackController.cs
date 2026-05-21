using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows a Player to control a weapon
/// </summary>
public class PlayerAttackController : AttackController
{
    InputAction attackAction;

    [SerializeField] CameraShaker playerCameraShaker;

    #region Initialization
    protected override void Awake()
    {
        base.Awake();
        attackAction = InputSystem.actions.FindAction("Attack");
        if (!playerCameraShaker)
        {
            playerCameraShaker = GetComponent<CameraShaker>();
        }
    }
    #endregion

    void HandleAttack()
    {
        if (attackAction.IsPressed() && GetSelectedWeapon() != null)
        {
            // TODO: CHECK IF ATTACK WAS SUCCESSFUL!
            playerCameraShaker.ShakeCamera(0.1f, GetSelectedWeapon().WeaponAttackShakeIntensity);
            Attack();
        }
    }

    void Update() => HandleAttack();
}
