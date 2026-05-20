using Unity.VisualScripting;
using UnityEngine;

public class LoadoutGiver : MonoBehaviour, IInteractable
{
    // TODO: Service for getting this
    [SerializeField] ProjectilePool projectilePool;
    [SerializeField] Loadout loadout;
    [SerializeField] HUDManager hUDManager;

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
        if (loadout.meleeWeapon != null)
        {
            meleeWeapon = Instantiate(loadout.meleeWeapon);

        }

        player.ReceiveLoadout(primaryWeapon, secondaryWeapon, meleeWeapon);
        hUDManager.SetSlotIcon(0, loadout.primaryWeapon.weaponIcon);
        hUDManager.SetSlotIcon(1, loadout.secondaryWeapon.weaponIcon);
        hUDManager.SetSlotIcon(2, loadout.meleeWeapon.weaponIcon);
    }
}
