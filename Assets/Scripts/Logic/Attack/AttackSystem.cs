using UnityEngine;
using UnityEngine.InputSystem;


public class AttackSystem : MonoBehaviour
{
    InputAction shootAction;
    public Weapon currentWeapon;

    //TODO: REMOVE THESE
    [SerializeField] GameObject DEBUG_MUZZLE_FLASH_LIGHT;
    [SerializeField] ParticleSystem DEBUG_MUZZLE_FLASH;

    void Awake()
    {
        DEBUG_MUZZLE_FLASH_LIGHT.SetActive(false);
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (shootAction.IsPressed())
        {
            DEBUG_MUZZLE_FLASH.Play();
            currentWeapon.Attack();
            DEBUG_MUZZLE_FLASH_LIGHT.SetActive(true);
        }
        else
        {
            currentWeapon.ActivateTriggerCollider(false);
            DEBUG_MUZZLE_FLASH_LIGHT.SetActive(false);
        }
    }
}
