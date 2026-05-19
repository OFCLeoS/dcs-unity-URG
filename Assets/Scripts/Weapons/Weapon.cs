using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Tooltip("The animator controller that this weapon uses")]
    [SerializeField] RuntimeAnimatorController animatorController;
    public RuntimeAnimatorController AnimatorController { get { return animatorController; } }

    [Tooltip("How intense will the camera shake be when attacking with this weapon")]
    [SerializeField] float weaponAttackShakeIntensity = 0.1f;
    public float WeaponAttackShakeIntensity { get { return weaponAttackShakeIntensity; } }

    [Tooltip("Where this weapon will be located in the carriers holster")]
    [SerializeField] Vector3 positionInHolster;
    public Vector3 PositionInHolster { get { return positionInHolster; } }

    [Tooltip("How this weapon will be rotated in the carriers holster")]
    [SerializeField] Vector3 rotationInHolster;
    public Vector3 RotationInHolster { get { return rotationInHolster; } }

    [Tooltip("Where this weapon will be located in the humanoids hand")]
    [SerializeField] Vector3 positionInHand;
    public Vector3 PositionInHand { get { return positionInHand; } }

    [Tooltip("How this weapon will be rotated in the humanoids hand")]
    [SerializeField] Vector3 rotationInHand;
    public Vector3 RotationInHand { get { return rotationInHand; } }

    public abstract void SetDamage(float newDamage);

    public abstract void Attack();
}