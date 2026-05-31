using UnityEngine;

#if UNITY_EDITOR
/// <summary>
/// Showcaser that will generate maps cinematically for displaying it in the trailer.
/// </summary>
public class MapGeneratorShowcaser : MonoBehaviour
{
    [SerializeField] MapGenerator mapGenerator;
    [SerializeField] int mapCount = 10;
    [SerializeField] float timeBetweenMaps = 0.3f;

    [SerializeField] Transform showcaseCam;

    [SerializeField] Vector3 cameraStartPos;
    [SerializeField] Vector3 cameraEndPos;

    [SerializeField] Transform rotationAxis;

    [SerializeField] float timeToCompleteSemiCircle = 3.9f;
    float timeLeft = 0;
    bool cameraMoving;

    Vector3 startPoint;
    Vector3 endPoint;

    Vector3 SemiCircleLerp(Vector3 a, Vector3 b, float t, Vector3 axis)
    {
        // midpoint between A and B
        Vector3 center = (a + b) * 0.5f;

        // vector from center to start
        Vector3 offset = a - center;

        // rotate from 0 -> 180 degrees
        Quaternion rot = Quaternion.AngleAxis(180f * t, axis.normalized);

        return center + rot * offset;
    }

    [ContextMenu("Start Showcase")]
    public void StartShowcase()
    {
        mapGenerator.StartMapGenShowcase(mapCount, timeBetweenMaps);
        cameraMoving = true;
        timeLeft = timeToCompleteSemiCircle;

        startPoint = cameraStartPos;
        endPoint = cameraEndPos;
    }

    void Update()
    {
        if (cameraMoving)
        {
            timeLeft -= Time.deltaTime;
            showcaseCam.localPosition = SemiCircleLerp(startPoint, endPoint, 1 - (timeLeft / timeToCompleteSemiCircle), rotationAxis.up);
            if (timeLeft <= 0)
            {
                timeLeft = timeToCompleteSemiCircle;

                Vector3 temp = startPoint;
                startPoint = endPoint;
                endPoint = temp;
            }
        }
    }
}

#endif