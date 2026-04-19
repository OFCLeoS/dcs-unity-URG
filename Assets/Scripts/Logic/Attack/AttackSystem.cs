using UnityEngine;
using UnityEngine.InputSystem;


public class AttackSystem : MonoBehaviour
{

    InputAction shootAction;
    InputAction meeleeAction;
    [SerializeField] GameObject projectile;

    [SerializeField] float spawnRate = 2f;
    [SerializeField] float delay = 1f;
    [SerializeField] float timer = 10f;
    [SerializeField] float rpm = 900f;

    // when using this weapon, shoot.
    public Weapon currentWeapon;
    GameObject newProjectile;
    float timeSinceLastRound;
    bool canFire;



    // translate

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        meeleeAction = InputSystem.actions.FindAction("Meelee");
    }

    void Update()
    {
        Attack();
    }


// line cast
    void Attack()
    {
        if(shootAction.IsPressed())
        {

            // currentWeapon.Attack(); use this method instead, bellow goes to each individual weapon

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
    }

    //void PerformMeleeAttack()
    //{
    //    if(meeleeAction.IsPressed())
    //    {
    //        // currentWeapon.Attack(); use this method instead but using the correct weapon
    //    }
    //}


    void SpawnProjectiles()
    {   

        // TODO see photo and remove timer.

        if(timer >= 0)
        {
            timer -= 0.1f;
        }
        else if(timer < 0)
        {
            //timer = maxTimer;
            newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            Destroy(newProjectile,delay);
        }
    }

}
