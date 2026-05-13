using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] MapLayout[] layouts;
    [SerializeField] Transform mapLayoutSpawnPoint;


    public void GenerateMap()
    {
        // TODO: SLOW INSTANTIATION!
        MapLayout generatedLayout = Instantiate(layouts[Random.Range(0, layouts.Length)], mapLayoutSpawnPoint.position, mapLayoutSpawnPoint.rotation);
        generatedLayout.InitializeLayout();
    }
}