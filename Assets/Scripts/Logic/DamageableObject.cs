using UnityEngine;

public abstract class DamageableObject : MonoBehaviour, IDamageable
{
    [Tooltip("The health this object starts at")]
    [SerializeField] protected float _health = 100;

    float currentHealth;
    protected bool isDestroyed = false;

    #region Initialization
    protected virtual void Awake()
    {
        if (_health <= 0)
        {
            Debug.LogError(name + "'s Object Health is not valid, the object will be destroyed...");
            isDestroyed = true;
            DestroyObject();
        }
        else currentHealth = _health;
    }
    #endregion

    public virtual void TakeDamage(float damageAmount)
    {
        Debug.Log(name+" took " +damageAmount+". Current Health: " +currentHealth);
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            isDestroyed = true;
            DestroyObject();
        }
    }

    public bool IsDestroyed => isDestroyed;

    protected abstract void DestroyObject();
}
