using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] float velocity = 10f;
    void Update()
    {
        TranslateProjectile();
    }

    void TranslateProjectile()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * velocity);
    }

}
