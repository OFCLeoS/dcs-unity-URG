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

    protected virtual void Awake()
    {
        if (selectedWeapon)
        {
            SetSelectedWeapon(selectedWeapon);
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

    public void SetSelectedWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(hand);
        weapon.transform.localPosition = weapon.PositionInHand;
        weapon.transform.localRotation = Quaternion.Euler(weapon.RotationInHand);
        animationController.ChangeAnimatorController(selectedWeapon.AnimatorController);
        selectedWeapon = weapon;
    }
}
