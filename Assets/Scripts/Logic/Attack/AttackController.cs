using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows an Entity to control a weapon
/// </summary>
public class AttackController : MonoBehaviour
{
    InputAction attackAction;
    public Weapon currentWeapon;

    void Awake() => attackAction = InputSystem.actions.FindAction("Attack");

    void HandleAttack()
    {
        if (attackAction.IsPressed())
        {
            currentWeapon.Attack();
        }
    }

    void Update()
    {
        HandleAttack();
    }
}
