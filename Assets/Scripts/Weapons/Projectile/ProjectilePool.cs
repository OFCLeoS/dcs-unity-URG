using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool of Projectiles that is responsible for filling projectile requests. Used for performance.
/// </summary>
public class ProjectilePool : MonoBehaviour
{
    [Tooltip("How many projectiles will this pool start with")]
    [SerializeField] uint _numberOfStartingProjectiles = 100;
    [SerializeField] Projectile projectile;

    Stack<Projectile> projectilePool = new Stack<Projectile>();

    #region Initialization
    void Awake()
    {
        InitializePool();
    }
    void InitializePool()
    {
        // We do this to avoid changing the hierarchy of the Projectiles
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        for (int i = 0; i < _numberOfStartingProjectiles; i++)
        {
            AddProjectileToPool(InstantiateNewProjectile());
        }
    }
    #endregion

    Projectile InstantiateNewProjectile()
    {
        Projectile newProjectile = Instantiate(projectile.gameObject, transform).GetComponent<Projectile>();
        newProjectile.SetProjectilePool(this);
        return newProjectile;
    }

    public void AddProjectileToPool(Projectile projectileToAdd) => projectilePool.Push(projectileToAdd);

    public void RequestProjectile(ProjectileBlueprint projectileBlueprint, Vector3 position, Quaternion rotation, float spread, Team attackingTeam)
    {
        Projectile projectileToUse;
        if (!projectilePool.TryPop(out projectileToUse))
        {
            projectileToUse = InstantiateNewProjectile();
        }
        projectileToUse.transform.position = position;
        projectileToUse.transform.rotation = rotation;
        projectileToUse.Activate(projectileBlueprint, spread, attackingTeam);
    }
}
