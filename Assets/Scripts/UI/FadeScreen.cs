using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] Image fadeScreen;
 

    /// <summary>
    /// Does a fade in step
    /// </summary>
    /// <returns>True if Fade In was completed</returns>
    public bool FadeInStep()
    {
        return false;
    }

    /// <summary>
    /// Does a fade out step
    /// </summary>
    /// <returns>True if Fade Out was completed</returns>
    public bool FadeOutStep()
    {
        return false;
    }
}
