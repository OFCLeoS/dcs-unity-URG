using UnityEngine;

public abstract class DamageableEntity : MonoBehaviour, IDamageable
{
    [Tooltip("The health this entity starts at")]
    [SerializeField] protected float _health = 100;

    float currentHealth;
    protected bool isDestroyed = false;

    #region Initialization
    protected virtual void Awake()
    {
        if (_health <= 0)
        {
            Debug.LogError(name + "'s Entity Health is not valid, the entity will be destroyed...");
            isDestroyed = true;
            DestroyEntity();
        }
        else currentHealth = _health;
    }
    #endregion

    public virtual void TakeDamage(float damageAmount)
    {
        // Debug.Log(name + " took " + damageAmount + ". Current Health: " + currentHealth);
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDestroyed = true;
            DestroyEntity();
        }
    }

    public bool IsDestroyed => isDestroyed;

    protected abstract void DestroyEntity();
}
