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
            //hUDManager.SetSlotIcon(0, loadout.primaryWeapon.weaponIcon);

        }
        if (loadout.secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(loadout.secondaryWeapon);
            secondaryWeapon.SetProjectilePool(projectilePool);
            //hUDManager.SetSlotIcon(1, loadout.secondaryWeapon.weaponIcon);
        }
        if (loadout.meleeWeapon != null)
        {
            meleeWeapon = Instantiate(loadout.meleeWeapon);
            //hUDManager.SetSlotIcon(2, loadout.meleeWeapon.weaponIcon);
        }

        player.ReceiveLoadout(primaryWeapon, secondaryWeapon, meleeWeapon);
    }
}
