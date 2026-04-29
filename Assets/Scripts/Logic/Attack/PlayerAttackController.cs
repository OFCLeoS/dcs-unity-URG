using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows a Player to control a weapon
/// </summary>
public class PlayerAttackController : AttackController
{
    InputAction attackAction;

    void Awake() => attackAction = InputSystem.actions.FindAction("Attack");

    void HandleAttack()
    {
        if (attackAction.IsPressed())
        {
            Attack();
        }
    }

    void Update() => HandleAttack();
}
