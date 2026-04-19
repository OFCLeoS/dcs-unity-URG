using UnityEngine;

public class LongRange : Weapon
{
    public override void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit; // ATINGIR qualquer cena e guardar informacoes
        SpawnProjectiles();
        if(Physics.Raycast(ray, out hit, 100))
        {
            Debug.Log("Acertei");
            Debug.DrawLine(ray.origin, ray.direction * 100, Color.pink);
        }
        else
        {
            Debug.Log("Failed");
            Debug.DrawRay(ray.origin,ray.direction * 100, Color.darkBlue);
        }
    }

    void SpawnProjectiles()
    {   

        // TODO see photo and remove timer.

        if(rpm >= 0)
        {
            rpm -= 0.1f;
        }
        else if(rpm < 0)
        {
            //timer = maxTimer;
            newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            Destroy(newProjectile,delay);
        }
    }
}
