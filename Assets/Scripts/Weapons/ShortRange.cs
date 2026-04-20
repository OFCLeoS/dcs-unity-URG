using UnityEngine;
using System.Collections;
using System;

public class ShortRange : Weapon
{
    Collider colliderAttack;


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
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("SHORT WEAPON: Enemy HIT");
        }
    }

    public override void activateTriggerCollider(bool triggerCollider)
    {
        colliderAttack.isTrigger = triggerCollider;
    }

}
