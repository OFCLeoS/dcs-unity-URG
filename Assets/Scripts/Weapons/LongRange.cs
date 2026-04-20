using UnityEngine;

public class LongRange : Weapon
{
    [SerializeField] protected GameObject projectile;
    [SerializeField] protected int magSize;
    [SerializeField] protected float rpm = 10f;
    [SerializeField] protected float destroyProjectileTime = 10f;
    [SerializeField] float projectileVelocity = 10f;
    [SerializeField] float bulletSpreadability = 1f;

    protected float fireDelay;
    float timeSinceLastRound;
    bool canFire;
    protected int magCurrentAmmount;
    protected GameObject newProjectile;

    public override void Attack()
    {
        fireDelay = 60f / rpm;
        timeSinceLastRound += Time.deltaTime;

        if(timeSinceLastRound >= fireDelay)
        {
            canFire = true;
        }

        if(!canFire) return;

        SpawnProjectiles();

        //Ray ray = new Ray(transform.position, transform.forward);
        //RaycastHit hit; // ATINGIR qualquer cena e guardar informacoes
//
        //if(Physics.Raycast(ray, out hit, 100))
        //{
        //    Debug.Log("Acertei");
        //    Debug.DrawLine(ray.origin, ray.direction * 100, Color.pink);
        //}
        //else
        //{
        //    Debug.Log("Failed");
        //    Debug.DrawRay(ray.origin,ray.direction * 100, Color.darkBlue);
        //}

        canFire = false;
        timeSinceLastRound = 0;

    }

    void SpawnProjectiles()
    {   
        newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        newProjectile.GetComponent<ProjectileBehaviour>().setVelocity(projectileVelocity);
        newProjectile.GetComponent<ProjectileBehaviour>().activateTriggerCollider(true);
        newProjectile.GetComponent<ProjectileBehaviour>().spreadBullets(bulletSpreadability);
        Destroy(newProjectile, destroyProjectileTime);

        // TODO see photo and remove tim        
        //if(rps >= 0)
        //{
        //    rps -= 0.1f;
        //}
        //else if(rps < 0)
        //{
        //    //timer = maxTimer;
        //    newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        //    Destroy(newProjectile,fireDelay);
        //}
    }



    public override void activateTriggerCollider(bool isColliderActivated)
    {
        return;
    }
}
