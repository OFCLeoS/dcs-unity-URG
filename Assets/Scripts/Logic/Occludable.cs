using UnityEngine;

public class Occludable : MonoBehaviour
{
    const float MIN_ALPHA = 0.15f;
    const float TIME_TO_FADE_IN = 1f;
    const float TIME_TO_FADE_OUT = 0.39f;

    static readonly int BaseColorID = Shader.PropertyToID("_Color");

    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock materialPropertyBlock;
    Color materialColour;

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
            color.a = Mathf.Lerp(1f, MIN_ALPHA, fadeProgress / TIME_TO_FADE_OUT);
            materialPropertyBlock.SetColor(BaseColorID, color);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
            if (fadeProgress >= TIME_TO_FADE_OUT)
            {
                enabled = false;
                fadeProgress = 0;
            }
        }
        else
        {
            Color color = materialColour;
            color.a = Mathf.Lerp(MIN_ALPHA, 1f, fadeProgress / TIME_TO_FADE_IN);
            materialPropertyBlock.SetColor(BaseColorID, color);
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
            if (fadeProgress >= TIME_TO_FADE_IN)
            {
                enabled = false;
                fadeProgress = 0;
            }
        }
    }
}