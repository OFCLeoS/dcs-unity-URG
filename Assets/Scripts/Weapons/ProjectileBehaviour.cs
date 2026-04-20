using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileBehaviour : MonoBehaviour
{
    // The first prefab needs to have a velocity of 0f so it doesnt moves
    float velocity = 0f;
    Collider colliderAttack;
    float bulletSpread;

    // Only works on Awake and not Start
    void Awake()
    {
        // The first prefab needs to have a isTrigger false so it wont be destroyed by accident
        colliderAttack = GetComponent<SphereCollider>();
        colliderAttack.isTrigger = false;
    }

    void Update()
    {
        TranslateProjectile();
    }

    void TranslateProjectile()
    {
        Vector3 shoot = new Vector3(Time.deltaTime * bulletSpread, 0f, Time.deltaTime * velocity);
        transform.Translate(shoot);
    }

    void OnTriggerEnter(Collider other)
    {


        if(other.CompareTag("Enemy"))
        {
            // deal damage
            Debug.Log($"Projectile {GetInstanceID()} hit {other.name}");
            Destroy(this.gameObject);
        }
        if(other.CompareTag("Player"))
        {
            // deal damage
            Debug.Log($"Projectile {GetInstanceID()} hit {other.name}");
            Destroy(this.gameObject);
        }
        if(other.CompareTag("Wall"))
        {
            Debug.Log($"Projectile {GetInstanceID()} hit {other.name}");
            Destroy(this.gameObject);
        }
    }


    // This is required because we can change velocity everytime we shoot
    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }

    // This is required because we can change the isTrigger everytime we shoot
    public void activateTriggerCollider(bool triggerCollider)
    {
        colliderAttack.isTrigger = triggerCollider;
    }

    public void spreadBullets(float bulletSpreadability)
    {
        float random = Random.Range(-bulletSpreadability, bulletSpreadability);
        this.bulletSpread = random;
    }

}
