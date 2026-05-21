using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] Image fadeScreen;

    float currentFadeTime;

    float currentFadeProgress;

    void Awake()
    {
        if (!fadeScreen)
        {
            fadeScreen = GetComponent<Image>();
        }
        Color fadeScreenColour = fadeScreen.color;
        fadeScreenColour.a = 0;
        fadeScreen.color = fadeScreenColour;
    }

    public void StartFade(float fadeInTime)
    {
        currentFadeTime = fadeInTime;
        currentFadeProgress = currentFadeTime;
    }

    /// <summary>
    /// Does a fade in step
    /// </summary>
    /// <returns>True if Fade Out was completed</returns>
    public bool FadeOutStep(float deltaTime)
    {
        currentFadeProgress -= deltaTime;
        Color fadeScreenColour = fadeScreen.color;
        fadeScreenColour.a = Mathf.Lerp(0, 1, 1 - (currentFadeProgress / currentFadeTime));
        fadeScreen.color = fadeScreenColour;

        if (currentFadeProgress <= 0) return true;
        else return false;
    }

    /// <summary>
    /// Does a fade out step
    /// </summary>
    /// <returns>True if Fade In was completed</returns>
    public bool FadeInStep(float deltaTime)
    {
        currentFadeProgress -= deltaTime;
        Color fadeScreenColour = fadeScreen.color;
        fadeScreenColour.a = Mathf.Lerp(1, 0, 1 - (currentFadeProgress / currentFadeTime));
        fadeScreen.color = fadeScreenColour;

        if (currentFadeProgress <= 0) return true;
        else return false;
    }
}
