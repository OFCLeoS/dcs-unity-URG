using UnityEngine;

public abstract class DamageableEntity : MonoBehaviour, IDamageable
{
    [Tooltip("The default max health of this entity")]
    [SerializeField] protected float _defaultMaxHealth = 100;

    float maxHealth;
    float currentHealth;
    protected bool isDestroyed = false;

    #region Initialization
    protected virtual void Awake()
    {
        if (_defaultMaxHealth <= 0)
        {
            Debug.LogError(name + "'s Entity Health is not valid, the entity will be destroyed...");
            isDestroyed = true;
            DestroyEntity();
        }
        else
        {
            maxHealth = _defaultMaxHealth;
            currentHealth = _defaultMaxHealth;
        }
    }
    #endregion

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDestroyed = true;
            DestroyEntity();
        }
        Debug.Log(name + " took " + damageAmount + ". Current Health: " + currentHealth);
    }

    public virtual void Heal(float healthToHeal)
    {
        if (healthToHeal < 0) healthToHeal = 0;

        currentHealth += healthToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    /// <summary>
    /// Subtracts the given number from the Entity's max health and sets their current health to match the new maximum if necessary.
    /// </summary>
    public void SubtractMaxHealth(float maxHealthToSubtract)
    {
        maxHealth -= maxHealthToSubtract;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    /// <summary>
    /// Adds the given number from the Entity's max health and sets their current health match the new maximum if their health was already at the maximum
    /// </summary>
    public void AddTakeMaxHealth(float maxHealthToAdd)
    {
        float oldMaxHealth = maxHealth;
        maxHealth += maxHealthToAdd;
        if (currentHealth == oldMaxHealth) currentHealth = maxHealth;
    }

    /// <summary>
    /// Sets this Entity's max health to it's default
    /// </summary>
    public void ResetMaxHealth() => maxHealth = _defaultMaxHealth;

    public bool IsDestroyed => isDestroyed;

    protected abstract void DestroyEntity();

    [ContextMenu("AAAA")]
    public void HEAL()
    {
        Heal(10);
    }

    [ContextMenu("AAAA2")]
    public void DAMAGE()
    {
        TakeDamage(10);
    }
}
