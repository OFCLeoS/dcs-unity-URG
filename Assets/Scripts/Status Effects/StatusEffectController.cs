using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    // TODO: REPLACE WITH BETTER PARENT CLASS!
    [SerializeField] DamageableEntity target;

    List<StatusEffect> activeEffects;

    void Awake()
    {
        if (!target)
        {
            target = GetComponent<DamageableEntity>();
        }
    }

}