using UnityEngine;
[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
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

    Team attackingTeam;

    SphereCollider startingTrigger;
    [Tooltip("How long the inital trigger will stay active for upon activation to make sure colliders inside projectile at spawn are registered (Should be very small)")]
    [SerializeField] float activeTriggerTime = 0.015f;
    float triggerTimeLeft;

    bool triggerEnabled = true;

    #region Initialization
    void Awake()
    {
        startingTrigger = GetComponent<SphereCollider>();
        startingTrigger.isTrigger = true;
        startingTrigger.enabled = false;

        Rigidbody projectileRB = GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;
        projectileRB.useGravity = false;

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

    public void Activate(ProjectileBlueprint projectileBlueprint, float spread, Team attackingTeam)
    {
        this.attackingTeam = attackingTeam;

        triggerTimeLeft = activeTriggerTime;
        triggerEnabled = true;
        startingTrigger.enabled = true;

        SetProjectileAttributes(projectileBlueprint, spread);
        lastPosition = transform.position;
        gameObject.SetActive(true);
    }

    void TranslateProjectile()
    {
        transform.Translate(Time.deltaTime * velocity);
    }

    void OnTriggerStay(Collider other)
    {
        HandleProjectileCollision(other);
    }

    void HandleProjectileCollision(Collider collider)
    {
        if (collider.CompareTag("Wall")) // TODO: CHANGE THIS!
        {
#if UNITY_EDITOR
            Debug.Log($"Projectile {GetInstanceID()} hit {collider.name}");
#endif
            Deactivate();
        }
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage, attackingTeam);
            Deactivate();
        }
    }

    /// <summary>
    /// Checks the projectile trajectory from last frame to the current one for collisions
    /// </summary>
    void CheckProjectileTrajectory()
    {
#if UNITY_EDITOR
        Debug.DrawLine(lastPosition, transform.position, Color.red, 0.5f);
#endif
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
        if (triggerEnabled)
        {
            triggerTimeLeft -= Time.deltaTime;
            if (triggerTimeLeft <= 0)
            {
                startingTrigger.enabled = false;
                triggerEnabled = false;
            }
        }
        TranslateProjectile();
        CheckProjectileTrajectory();
        lastPosition = transform.position;
    }
}
