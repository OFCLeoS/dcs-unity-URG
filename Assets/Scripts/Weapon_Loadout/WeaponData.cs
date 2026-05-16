using System;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    [SerializeField] private string weaponName;
    [SerializeField] private int damage;
    [SerializeField] private Sprite weaponIcon;

    public String getWeaponName()
    {
        return weaponName;
    }

    public int getDamage()
    {
        return damage;
    }

    public Sprite getWeaponIcon()
    {
        return weaponIcon;
    }
}
