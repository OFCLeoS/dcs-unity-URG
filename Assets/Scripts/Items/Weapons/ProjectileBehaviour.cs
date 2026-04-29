using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] Vector3 velocity;

    [SerializeField] float destroyProjectileTime = 10f; // PUT IN PROJECTILE

    [SerializeField] float damage = 50;

    void TranslateProjectile()
    {
        transform.Translate(Time.deltaTime * velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        HandleProjectileCollision(other);
    }

    void HandleProjectileCollision(Collider collider)
    {
        if (collider.CompareTag("Wall")) // TODO: CHANGE THIS!
        {
            Debug.Log($"Projectile {GetInstanceID()} hit {collider.name}");
            Destroy(gameObject);
        }
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void AddVelocity(Vector3 addedVelocity)
    {
        velocity += addedVelocity;
    }

    void Update()
    {
        TranslateProjectile();
    }
}
