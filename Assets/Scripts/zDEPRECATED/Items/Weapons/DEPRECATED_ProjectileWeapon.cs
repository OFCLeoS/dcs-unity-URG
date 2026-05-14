using UnityEngine;

/// <summary>
/// A weapon that launches projectiles that damage enemies
/// </summary>
[System.Obsolete("This Uses a Deprecated Inventory System, DO NOT USE!")]
public class DEPRECATED_ProjectileWeapon : Weapon
{
    [SerializeField] GameObject projectile;
    [SerializeField] int weaponCapacity;
    [SerializeField] float rpm = 700f;

    [Tooltip("The max and min spread on the x axis the bullet will have")]
    [SerializeField] float bulletSpreadability = 0.39f;

    [Tooltip("Projectiles will come out from this transform's foward vector.")]
    [SerializeField] Transform weaponBarrel;

    [Tooltip("Shell casings will come out from this transform's right vector.")]
    [SerializeField] Transform shellEjector;
    [SerializeField] GameObject shellCasing;

    [SerializeField] GameObject muzzleFlashLight;
    [Tooltip("How long the muzzle flash will be active for upon firing.")]
    [SerializeField] float muzzleFlashActiveTime;
    float timeBeforeMuzzleFlashDeactivation;
    bool muzzleFlashActive = false;

    [SerializeField] ParticleSystem muzzleFlashParticle;

    /// <summary>
    /// The delay between shots in seconds 
    /// </summary>
    float fireDelay;
    float timeBeforeAbleToFire;
    bool canFire;

    int currentWeaponAmmo;

    #region Initialization
    void Awake()
    {
        fireDelay = 60f / rpm;
    }
    #endregion

    public override void Attack()
    {
        // fireDelay = 60f / rpm; UNCOMMENT FOR TESTING
        if (!canFire) return;

        muzzleFlashLight.SetActive(true);
        timeBeforeMuzzleFlashDeactivation = muzzleFlashActiveTime;
        muzzleFlashActive = true;

        muzzleFlashParticle.Play();
        SpawnProjectiles();

        canFire = false;
        timeBeforeAbleToFire = fireDelay;
    }

    void SpawnProjectiles()
    {
        float randomSpread = Random.Range(-bulletSpreadability, bulletSpreadability);

        GameObject newProjectile = Instantiate(projectile, weaponBarrel.position, weaponBarrel.rotation);
        newProjectile.GetComponent<DEPRECATED_ProjectileBehaviour>().AddVelocity(Vector3.right * randomSpread);

        GameObject shellCasingInstance = Instantiate(shellCasing, shellEjector.position, shellEjector.transform.rotation);
        shellCasingInstance.GetComponent<Rigidbody>().AddForce((-shellEjector.right * Random.Range(100, 176)) + (shellEjector.forward * Random.Range(-5, 5)));
        // TODO: THIS IS TEMPORARY
        Destroy(shellCasingInstance, 60);
    }

    void Update()
    {
        if (!canFire)
        {
            timeBeforeAbleToFire -= Time.deltaTime;
            if (timeBeforeAbleToFire <= 0) canFire = true;
        }
        if (muzzleFlashActive)
        {
            if (timeBeforeMuzzleFlashDeactivation <= 0)
            {
                muzzleFlashLight.SetActive(false);
                muzzleFlashActive = false;
            }
            timeBeforeMuzzleFlashDeactivation -= Time.deltaTime;
        }
    }

    public override void SetDamage(float newDamage)
    {
        throw new System.NotImplementedException();
    }
}
