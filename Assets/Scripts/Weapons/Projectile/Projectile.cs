using UnityEngine;

public class Projectile : MonoBehaviour
{
    /// <summary>
    /// The Projectile Pool this Projectile will return to once used.
    /// </summary>
    ProjectilePool projectilePool;

    Vector3 velocity;
    float destroyProjectileTime;
    float damage;

    Vector3 lastPosition;

    #region Initialization
    void Awake()
    {
        gameObject.SetActive(false);
    }
    public void SetProjectilePool(ProjectilePool projectilePool)
    {
        this.projectilePool = projectilePool;
    }
    #endregion

    void SetProjectileAttributes(ProjectileBlueprint projectileBlueprint, float spread)
    {
        velocity = Vector3.forward * projectileBlueprint.velocity;
        velocity.x += spread;
        
        destroyProjectileTime = projectileBlueprint.destroyProjectileTime;
        damage = projectileBlueprint.damage;
    }

    public void Activate(ProjectileBlueprint projectileBlueprint, float spread)
    {
        SetProjectileAttributes(projectileBlueprint, spread);
        lastPosition = transform.position;
        gameObject.SetActive(true);
    }

    void TranslateProjectile()
    {
        transform.Translate(Time.deltaTime * velocity);
    }

    void HandleProjectileCollision(Collider collider)
    {
        if (collider.CompareTag("Wall")) // TODO: CHANGE THIS!
        {
            Debug.Log($"Projectile {GetInstanceID()} hit {collider.name}");
            Deactivate();
        }
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Deactivate();
        }
    }

    /// <summary>
    /// Checks the projectile trajectory from last frame to the current one for collisions
    /// </summary>
    void CheckProjectileTrajectory()
    {
        Debug.DrawRay(lastPosition, transform.position, Color.red, 0.5f);
        RaycastHit hit;
        // TODO: LAYER MASK FOR BETTER PERFORMANCE?
        if (Physics.Linecast(lastPosition, transform.position, out hit))
        {
            HandleProjectileCollision(hit.collider);
        }
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
        projectilePool.AddProjectileToPool(this);
    }

    void Update()
    {
        TranslateProjectile();
        CheckProjectileTrajectory();
        lastPosition = transform.position;
    }
}
