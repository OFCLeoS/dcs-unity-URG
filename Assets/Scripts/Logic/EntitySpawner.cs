using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject SpawnEntity(GameObject entity)
    {
        return Instantiate(entity,transform.position,Quaternion.identity);
    }
}