using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int magSize;
    [SerializeField] protected float rpm;
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected float delay = 1f;

    protected int magCurrentAmmount;
    protected GameObject newProjectile;
    public abstract void Attack();
}
