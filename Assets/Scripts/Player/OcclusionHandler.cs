using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manager responsible for occluding occludable objects that may be hindering the camera from viewing the player
/// </summary>
public class OcclusionHandler : MonoBehaviour
{
    readonly RaycastHit[] results = new RaycastHit[32];

    [SerializeField] Transform player;
    [SerializeField] Transform playerCamera;
    [SerializeField] LayerMask occludableLayer;

    HashSet<Transform> currentHits = new HashSet<Transform>();
    HashSet<Transform> previousHits = new HashSet<Transform>();

    void Awake()
    {
        occludableLayer = LayerMask.GetMask("Occludable");
    }

    // Needed:
    // Occlude obvious
    // Occlude room player is looking at?
    void HandleOcclusion()
    {
        Vector3 startPosition = playerCamera.position;
        Vector3 targetPosition = player.position;

        // TODO: CHECK IF 1.5f IS THE BEST VALUE
        Vector3 direction = (targetPosition-(Vector3.forward*1.5f) - startPosition).normalized;

        Vector3 halfExtents = new Vector3(2.5f, 1f, 0.1f);

        currentHits.Clear();

        int hitCount = Physics.BoxCastNonAlloc(startPosition, halfExtents, direction, results, Quaternion.identity, 200, occludableLayer);

        for (int i = 0; i < hitCount; i++)
        {
            Transform hitTransform = results[i].transform;
            currentHits.Add(hitTransform);

            if (!previousHits.Remove(hitTransform))
            {
                hitTransform.GetComponent<Occludable>().Occlude();
            }
        }

        foreach (Transform previousHit in previousHits)
        {
            previousHit.GetComponent<Occludable>().UnOcclude();
        }

        HashSet<Transform> temp = previousHits;
        previousHits = currentHits;
        currentHits = temp;
    }

    // TODO: DONT DO THIS PER FRAME???
    void Update()
    {
        HandleOcclusion();
    }
}