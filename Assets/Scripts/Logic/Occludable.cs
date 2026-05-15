using UnityEngine;

public class Occludable : MonoBehaviour
{
    static readonly int BaseColorID = Shader.PropertyToID("_Color");

    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    Color materialColour;

    const float minAlpha = 0.15f;
    const float timeToFadeIn = 1f;
    const float timeToFadeOut = 0.39f;

    bool isFadingOut;
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialPropertyBlock = new MaterialPropertyBlock();

        materialColour = meshRenderer.material.color;
        enabled = false;
    }

    public void Occlude()
    {
        enabled = true;
        isFadingOut = true;
        fadeProgress = 0;
    }

    public void UnOcclude()
    {
        enabled = true;
        isFadingOut = false;
        fadeProgress = 0;
    }

    float fadeProgress = 0;

    void Update()
    {
        fadeProgress += Time.deltaTime;
        if (isFadingOut)
        {
            Color color = materialColour;
            color.a = Mathf.Lerp(1f, minAlpha, fadeProgress / timeToFadeOut);
            materialPropertyBlock.SetColor(BaseColorID, color);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
            if (fadeProgress >= timeToFadeOut)
            {
                enabled = false;
                fadeProgress = 0;
            }
        }
        else
        {
            Color color = materialColour;
            color.a = Mathf.Lerp(minAlpha, 1f, fadeProgress / timeToFadeIn);
            materialPropertyBlock.SetColor(BaseColorID, color);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
            if (fadeProgress >= timeToFadeIn)
            {
                enabled = false;
                fadeProgress = 0;
            }
        }
    }
}