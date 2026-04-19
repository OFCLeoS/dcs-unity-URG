using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float velocity = 10f;
    //[SerializeField] GameObject enemy;

    void Update()
    {
        TranslateProjectile();
        
    }

    void TranslateProjectile()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            Debug.Log("EXPLOSAOOO");
            Destroy(this.gameObject);
        }
    }

}
