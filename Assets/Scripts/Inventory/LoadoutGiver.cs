using UnityEngine;

public class LoadoutGiver : MonoBehaviour, IInteractable
{
    // TODO: Service for getting this
    [SerializeField] ProjectilePool projectilePool;
    [SerializeField] Loadout loadout;

    public void OnInteract(Player player)
    {
        ProjectileWeapon primaryWeapon = null;
        ProjectileWeapon secondaryWeapon = null;
        MeleeWeapon meleeWeapon = null;

        if (loadout.primaryWeapon != null)
        {
            primaryWeapon = Instantiate(loadout.primaryWeapon);
            primaryWeapon.SetProjectilePool(projectilePool);
        }
        if (loadout.secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(loadout.secondaryWeapon);
            secondaryWeapon.SetProjectilePool(projectilePool);
        }
        if (loadout.meleeWeapon != null) meleeWeapon = Instantiate(loadout.meleeWeapon);

        player.ReceiveLoadout(primaryWeapon, secondaryWeapon, meleeWeapon);
    }
}
