using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack();
    public abstract void activateTriggerCollider(bool isColliderActivated);
}
