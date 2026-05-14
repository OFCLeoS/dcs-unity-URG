using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    // TODO: REPLACE WITH BETTER PARENT CLASS!
    [SerializeField] DamageableEntity target;

    List<DebuffStatusEffect> activeDebuffs = new List<DebuffStatusEffect>();
    List<RecurrentStatusEffect> activeRecurrentEffects = new List<RecurrentStatusEffect>();

    void Awake()
    {
        if (!target)
        {
            target = GetComponent<DamageableEntity>();
        }
    }

    public void AddDebuff(DebuffStatusEffect debuff)
    {
        activeDebuffs.Add(debuff);
        debuff.ApplyEffect();
    }

}