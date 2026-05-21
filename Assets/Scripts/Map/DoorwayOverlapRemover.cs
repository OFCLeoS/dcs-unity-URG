using UnityEngine;

/// <summary>
/// Responsible for checking if a doorway exists and for removing certain objects if that is the case
/// </summary>
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class DoorwayOverlapRemover : MonoBehaviour
{
    [SerializeField] BoxCollider overlapChecker;
    [SerializeField] GameObject[] objectsToRemoveOnDoorwayDetection;

    public void HandleOverlap()
    {
        Collider[] overlaps = Physics.OverlapBox(
            transform.position,
            new Vector3(
                (overlapChecker.size.x * transform.localScale.x) / 2.0f,
                (overlapChecker.size.y * transform.localScale.y) / 2.0f,
                (overlapChecker.size.z * transform.localScale.z) / 2.0f),
            transform.rotation);

        // If no collision is found, a door is there
        for (int i = 0; i < overlaps.Length; i++)
        {
            // If a wall is found, overlaps are not going to be removed
            if (overlaps[i].transform.root.Equals(transform.root) && overlaps[i].tag.Equals("Wall"))
            {
                Destroy(gameObject);
                return;
            }
        }
        for (int i = 0; i < objectsToRemoveOnDoorwayDetection.Length; i++)
        {
            if (objectsToRemoveOnDoorwayDetection[i] != null) Destroy(objectsToRemoveOnDoorwayDetection[i]);
        }
        Destroy(gameObject);
    }
}