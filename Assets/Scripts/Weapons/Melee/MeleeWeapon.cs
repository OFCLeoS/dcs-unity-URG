using UnityEngine;

/// <summary>
/// A weapon that deals damage to nearby damageables
/// </summary>
public class MeleeWeapon : Weapon
{
    Collider attackCollider;
    [SerializeField] float damage = 115;

    void Awake()
    {
        attackCollider = GetComponent<BoxCollider>();
        attackCollider.isTrigger = true;
        attackCollider.enabled = false;
    }

    public override void SetDamage(float newDamage) => damage = newDamage;

    public override void Attack()
    {
        if (DEBUG_CAN_ATTACK)
        {
            DEBUG_CAN_ATTACK = false;
            DEBUG_IS_ATTACKING = true;
            DEBUG_TIME_BEFORE_ATTACK_PERFORMED = DEBUG_TIME_UNTIL_ATTACK_PERFORMED;
            DEBUG_TIME_BEFORE_ABLE_TO_ATTACK = DEBUG_ATTACK_COOLDOWN;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        HandleMeleeCollision(other);
    }

    void HandleMeleeCollision(Collider collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(damage);
    }

    #region DEBUGGING
    bool DEBUG_CAN_ATTACK = true;
    [SerializeField] float DEBUG_TIME_UNTIL_ATTACK_PERFORMED = 0.5f;
    [SerializeField] float DEBUG_ATTACK_COOLDOWN = 1f;

    float DEBUG_TIME_BEFORE_ATTACK_PERFORMED = 0;
    float DEBUG_TIME_BEFORE_ABLE_TO_ATTACK = 0;
    bool DEBUG_IS_ATTACKING = false;
    bool DEBUG_HAS_ATTACKED = false;

    void DEBUG_ATTACK_SYSTEM()
    {
        if (DEBUG_IS_ATTACKING)
        {
            if (DEBUG_TIME_BEFORE_ATTACK_PERFORMED <= 0)
            {
                attackCollider.enabled = true;
                DEBUG_HAS_ATTACKED = true;
                DEBUG_IS_ATTACKING = false;
            }
            DEBUG_TIME_BEFORE_ATTACK_PERFORMED -= Time.deltaTime;
        }
        else if (DEBUG_HAS_ATTACKED)
        {
            attackCollider.enabled = false;
            DEBUG_HAS_ATTACKED = false;
        }
        else if (DEBUG_TIME_BEFORE_ABLE_TO_ATTACK > 0)
        {
            DEBUG_TIME_BEFORE_ABLE_TO_ATTACK -= Time.deltaTime;
            if (DEBUG_TIME_BEFORE_ABLE_TO_ATTACK <= 0)
            {
                DEBUG_CAN_ATTACK = true;
            }
        }
    }
    #endregion

    void Update()
    {
        DEBUG_ATTACK_SYSTEM();
    }
}
