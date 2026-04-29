using UnityEngine;

/// <summary>
/// Allows an Entity to control a weapon
/// </summary>
public class AttackController : MonoBehaviour
{
    [SerializeField] Weapon selectedWeapon;

    protected void Attack() => selectedWeapon.Attack();
}
