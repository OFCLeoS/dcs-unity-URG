using System;
using UnityEngine;

[Serializable]
public struct Loadout
{
    public ProjectileWeapon primaryWeapon;
    public ProjectileWeapon secondaryWeapon;
    public MeleeWeapon meleeWeapon;
    // TODO: Special Tactical Slot?
}
