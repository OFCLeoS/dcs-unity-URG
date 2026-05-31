using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible to feed information from the melee box to the melee weapon for proper hit registers
/// </summary>
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class MeleeBox : MonoBehaviour
{
    BoxCollider box;
    MeleeWeapon equippedMeleeWeapon;

    HashSet<Collider> collidersHitThisAttack = new HashSet<Collider>();

    void Awake()
    {
        Rigidbody boxRB = GetComponent<Rigidbody>();
        boxRB.isKinematic = true;
        boxRB.useGravity = false;

        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
        box.enabled = false;
    }

    public void DisableMeleeBox()
    {
        box.enabled = false;
    }

    public void EnableMeleeBox(MeleeWeapon equippedMeleeWeapon)
    {
        this.equippedMeleeWeapon = equippedMeleeWeapon;
        collidersHitThisAttack.Clear();
        box.enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!collidersHitThisAttack.Contains(other))
        {
            collidersHitThisAttack.Add(other);
            equippedMeleeWeapon.HandleMeleeCollision(other);
        }
    }
}
