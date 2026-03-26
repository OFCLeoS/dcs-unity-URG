using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{

    InputAction shootAction;
    [SerializeField] GameObject projectile;
    [SerializeField] float spawnRate = 2f;
    [SerializeField] float delay = 1f;
    [SerializeField] float timer = 10f;

    [SerializeField] float rpm = 900f;



    // translate

    void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        ShootRaycast();
    }


// line cast
    void ShootRaycast()
    {
        if(shootAction.IsPressed())
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
    }

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
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            Destroy(newProjectile,delay);
        }
    }

}
