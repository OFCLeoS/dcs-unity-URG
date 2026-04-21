using UnityEngine;
using UnityEngine.InputSystem;


public class AttackSystem : MonoBehaviour
{

    InputAction shootAction;
    public Weapon currentWeapon;

    void Awake()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if(shootAction.IsPressed())
        {
            currentWeapon.Attack();
        }
        else
        {
            currentWeapon.ActivateTriggerCollider(false);
        }
    }
}
