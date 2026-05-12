using UnityEngine;

public class Occludable: MonoBehaviour
{
    MeshRenderer meshRenderer;
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Occlude()
    {
        meshRenderer.enabled = false;
    }

    public void UnOcclude()
    {
        meshRenderer.enabled = true;
    }
}