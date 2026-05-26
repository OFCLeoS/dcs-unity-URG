using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    [SerializeField] FadeScreen fadeScreen;
    [SerializeField] float fadeOutTime;

    [SerializeField] CinemachineVirtualCamera playerCamera;
    CinemachineFramingTransposer framingTransposer;
    [SerializeField] float startingCameraDistance;
    [SerializeField] float endingCameraDistance;

    [SerializeField] Vector3 startingCameraRotation;
    [SerializeField] Vector3 endingCameraRotation;

    bool isActive = false;
    [SerializeField] string mainMenuSceneName = "MainMenuScene";

    [SerializeField] float cameraPanTime;
    float timeLeft;
    void Awake()
    {
        isActive = false;
        enabled = false;
        framingTransposer = playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    public void StartDeathSequence()
    {
        fadeScreen.StartFade(fadeOutTime);
        isActive = true;
        enabled = true;
        timeLeft = cameraPanTime;
    }

    void HandlePlayerDeath()
    {
        timeLeft -= Time.deltaTime;
        framingTransposer.m_CameraDistance = Mathf.Lerp(startingCameraDistance, endingCameraDistance, 1 - (timeLeft / cameraPanTime));
        playerCamera.transform.eulerAngles = Vector3.Slerp(startingCameraRotation, endingCameraRotation, 1 - (timeLeft / cameraPanTime));
        if (fadeScreen.FadeOutStep(Time.deltaTime))
        {
            SceneManager.LoadScene(mainMenuSceneName, LoadSceneMode.Single);
        }
    }

    void Update()
    {
        HandlePlayerDeath();
    }
}
