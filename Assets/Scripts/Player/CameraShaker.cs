using Cinemachine;
using UnityEngine;

/// <summary>
/// Shakes the player camera
/// </summary>
public class CameraShaker : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    [SerializeField] float defaultAmplitudeGain = 0.39f;
    [SerializeField] float defaultFrequencyGain = 3.9f;

    bool isShaking = true;

    #region Initialization
    void Awake()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Start()
    {
        virtualCameraNoise.enabled = false;
        enabled = false;
    }
    #endregion

    public void ShakeCamera(float shakeTime) => ShakeCamera(shakeTime, defaultAmplitudeGain, defaultFrequencyGain);

    public void ShakeCamera(float shakeTime, float amplitudeGain) => ShakeCamera(shakeTime, amplitudeGain, defaultFrequencyGain);

    public void ShakeCamera(float shakeTime, float amplitudeGain, float frequencyGain)
    {
        Debug.Log("HIIIII");
        virtualCameraNoise.m_AmplitudeGain = amplitudeGain;
        virtualCameraNoise.m_FrequencyGain = frequencyGain;
        shakeTimer = shakeTime;

        isShaking = true;
        virtualCameraNoise.enabled = true;
        enabled = true;
    }


    float shakeTimer = 0;
    void Update()
    {
        if (isShaking)
        {
            Debug.Log("WASSAP");
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                virtualCameraNoise.enabled = false;
                isShaking = false;
                enabled = false;
            }
        }
    }
}