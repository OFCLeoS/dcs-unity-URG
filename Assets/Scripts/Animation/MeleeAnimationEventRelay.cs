using UnityEngine;

/// <summary>
/// Relays the melee animation events to a melee box.
/// </summary>
public class MeleeAnimationEventRelay : MonoBehaviour
{
    [SerializeField] MeleeWeapon meleeWeapon;

    public void SetMeleeWeapon(MeleeWeapon meleeWeapon) => this.meleeWeapon = meleeWeapon;

    [SerializeField] MeleeBox meleeBox;

    public void StartAttack()
    {
        meleeBox.EnableMeleeBox(meleeWeapon);
    }

    public void EndAttack()
    {
        meleeBox.DisableMeleeBox();
        meleeWeapon.EndAttack();
    }
}