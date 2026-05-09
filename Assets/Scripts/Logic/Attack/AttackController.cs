using UnityEngine;

/// <summary>
/// Allows an Entity to control a weapon
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon selectedWeapon;

    public void Attack() => selectedWeapon.Attack();

    public void SetSelectedWeapon(Weapon weapon) => selectedWeapon = weapon;
}
