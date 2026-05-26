
using UnityEngine;

public static class StatusEffectFactory
{
    public static DebuffStatusEffect CreateRandomDebuff(DamageableEntity target)
    {
        // TODO: CHANGE THIS!!!
        switch (Random.Range(0, 2))
        {
            default: return new WoundedDebuff(target, 15);
        }
    }
}