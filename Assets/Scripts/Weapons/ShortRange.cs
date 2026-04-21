using UnityEngine;
using System.Collections;
using System;

public class ShortRange : Weapon
{
    Collider colliderAttack;
    [SerializeField] float damage = 115;


    //public LayerMask enemyLayerMask;

    void Start()
    {
        colliderAttack = GetComponent<BoxCollider>();
    }

    public override void Attack()
    {
        colliderAttack.isTrigger = true;
    }
    
    void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(damage);
    }

    public override void ActivateTriggerCollider(bool triggerCollider)
    {
        colliderAttack.isTrigger = triggerCollider;
    }

}
