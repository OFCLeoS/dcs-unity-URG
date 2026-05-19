using UnityEngine;

/// <summary>
/// Allows an Entity to control a weapon
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon selectedWeapon;
    [Tooltip("The hand the weapon will be parented to")]
    [SerializeField] Transform hand;

    public void Attack() => selectedWeapon.Attack();

    public Weapon GetSelectedWeapon() => selectedWeapon;

    public void SetSelectedWeapon(Weapon weapon)
    {
        weapon.transform.SetParent(hand);
        weapon.transform.localPosition = weapon.PositionInHand;
        weapon.transform.localRotation = Quaternion.Euler(weapon.RotationInHand);
        selectedWeapon = weapon;
    }
}
