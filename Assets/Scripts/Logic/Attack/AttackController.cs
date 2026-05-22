using UnityEngine;

/// <summary>
/// Allows an Entity to control a weapon
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField] Team controllerTeam;
    [SerializeField] Weapon selectedWeapon;
    [Tooltip("The hand the weapon will be parented to")]
    [SerializeField] Transform hand;
    [SerializeField] HumanoidAnimationController animationController;
    [SerializeField] HUDManager hUDManager;
    Sprite empty = null;


    protected virtual void Awake()
    {
        if (selectedWeapon)
        {
            SetSelectedWeapon(-1, empty, selectedWeapon);
        }
    }

    public void Attack()
    {
        if (selectedWeapon.Attack(controllerTeam))
        {
            animationController.AttackAnimation();
        }
    }

    public Weapon GetSelectedWeapon() => selectedWeapon;

    public void SetSelectedWeapon(int index, Sprite weaponIcon, Weapon weapon)
    {
        weapon.transform.SetParent(hand);
        weapon.transform.localPosition = weapon.PositionInHand;
        weapon.transform.localRotation = Quaternion.Euler(weapon.RotationInHand);
        selectedWeapon = weapon;
        animationController.ChangeAnimatorController(selectedWeapon.AnimatorController);
        if(index != -1)
        {
            hUDManager.SetSelectedSlot(index);
            hUDManager.SetEquippedIcon(weaponIcon);
        }
    }
}
