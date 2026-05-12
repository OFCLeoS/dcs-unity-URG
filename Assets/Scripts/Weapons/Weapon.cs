using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("How intense will the camera shake be when attacking with this weapon")]
    [SerializeField] float weaponAttackShakeIntensity = 0.1f;
    public float WeaponAttackShakeIntensity { get { return weaponAttackShakeIntensity; } }
    
    public abstract void Attack();
}