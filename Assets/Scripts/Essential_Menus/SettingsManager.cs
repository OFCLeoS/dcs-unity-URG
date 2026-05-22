using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SettingsManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private TMP_Text masterVolumeLabel;

    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Mouse")]
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TMP_Text mouseSensitivityLabel;

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;

    private SettingsData currentSettings;
    private Resolution[] availableResolutions;

    [SerializeField] PlayerRotation playerRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ApplySettings();
    }

    private void OnEnable()
    {
        currentSettings = SettingsData.Load() ?? new SettingsData();
        PopulateResolutionDropdown();
        ApplySettingsToUI();
        ApplySettings();
    }

    private void PopulateResolutionDropdown()
    {
        if (resolutionDropdown == null)
        {
            return;
        }

        availableResolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        var options = new System.Collections.Generic.List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            //foreach (var res in availableResolutions)
            //{
            //options.Add(res.width + " x " + res.height);
            options.Add(availableResolutions[i].width + " x " + availableResolutions[i].height);

            if (availableResolutions[i].width == Screen.currentResolution.width &&
                availableResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        currentSettings.resolutionIndex = currentResolutionIndex;
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void ApplySettingsToUI()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = currentSettings.masterVolume * 100f;
        }
        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.value = currentSettings.mouseSensitivity;
        }
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = currentSettings.fullscreen;
        }

        if (masterVolumeLabel != null)
        {
            masterVolumeLabel.text = Mathf.RoundToInt(currentSettings.masterVolume * 100f) + "%";
        }
        if (mouseSensitivityLabel != null)
        {
            mouseSensitivityLabel.text = Mathf.RoundToInt(currentSettings.mouseSensitivity).ToString();
        }
    }

    private void ApplySettings()
    {
        SetMixerVolume("MasterVolume", currentSettings.masterVolume);
        Screen.fullScreen = currentSettings.fullscreen;

        if (availableResolutions != null &&
            currentSettings.resolutionIndex < availableResolutions.Length)
        {
            Resolution res = availableResolutions[currentSettings.resolutionIndex];
            Screen.SetResolution(res.width, res.height, currentSettings.fullscreen);
        }
        playerRotation.SetSensitivity(SettingsData.Load().mouseSensitivity / 100f);
    }

    private void SetMixerVolume(string parameter, float linearValue)
    {
        if (audioMixer == null)
        {
            return;
        }
        float db = linearValue > 0.001f ? Mathf.Log10(linearValue) * 20f : -80f;
        audioMixer.SetFloat(parameter, db);
    }

    public void OnMasterVolumeChanged(float value)
    {
        float normalized = value / 100f;
        currentSettings.masterVolume = normalized;
        SetMixerVolume("MasterVolume", normalized);
        if (masterVolumeLabel != null)
        {
            masterVolumeLabel.text = Mathf.RoundToInt(value) + "%";
        }
    }

    public void OnResolutionChanged(int index)
    {
        currentSettings.resolutionIndex = index;
        if (availableResolutions != null && index < availableResolutions.Length)
        {
            Resolution res = availableResolutions[index];
            Screen.SetResolution(res.width, res.height, currentSettings.fullscreen);
        }
    }

    public void OnFullscreenToggled(bool isFullscreen)
    {
        currentSettings.fullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void OnMouseSensitivityChanged(float value)
    {
        currentSettings.mouseSensitivity = value;
        if (mouseSensitivityLabel != null)
        {
            mouseSensitivityLabel.text = Mathf.RoundToInt(value).ToString();
        }
    }

    public void OnApply()
    {
        currentSettings.Save();
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        ApplySettings();
    }

    public void OnCancel()
    {
        currentSettings = SettingsData.Load() ?? new SettingsData();
        ApplySettingsToUI();
        ApplySettings();
        menuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void OnResetToDefaults()
    {
        SettingsData.DeleteSave();
        currentSettings = new SettingsData();
        ApplySettingsToUI();
        ApplySettings();
    }
}
