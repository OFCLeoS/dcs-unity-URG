using UnityEngine;

[System.Serializable]
public class LoadoutData
{
    [SerializeField] private string loadoutName;
    [SerializeField] private WeaponData primary;
    [SerializeField] private WeaponData secondary;
    [SerializeField] private WeaponData melee;
    [SerializeField] private WeaponData utility;

    public string getLoadoutName()
    {
        return loadoutName;
    }

    public WeaponData getPrimary()
    {
        return primary;
    }

    public WeaponData getSecondary()
    {
        return secondary;
    }

    public WeaponData getMelee()
    {
        return melee;
    }

    public WeaponData getUtility()
    {
        return utility;
    }
}
