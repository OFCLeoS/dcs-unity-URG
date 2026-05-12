using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    private SettingsData currentSettings;
    private Resolution[] availableResolutions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        foreach (var res in availableResolutions)
        {
            options.Add(res.width + " x " + res.height);
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = Mathf.Clamp(currentSettings.resolutionIndex, 
            0, availableResolutions.Length - 1);
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
        gameObject.SetActive(false);
    }

    public void OnCancel()
    {
        currentSettings = SettingsData.Load() ?? new SettingsData();
        ApplySettingsToUI();
        ApplySettings();
        gameObject.SetActive(false);
    }

    public void OnResetToDefaults()
    {
        SettingsData.DeleteSave();
        currentSettings = new SettingsData();
        ApplySettingsToUI();
        ApplySettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
