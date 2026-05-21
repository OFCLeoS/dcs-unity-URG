using UnityEngine;

public class SettingsData
{
    public float masterVolume = 1f; // saved as 0-1 internally
    public float mouseSensitivity = 50f; // saved as 0-100
    public bool fullscreen = true;
    public int resolutionIndex = 0;

    private const string PrefsKey = "GameSettings";

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PrefsKey, json);
        PlayerPrefs.Save();
        Debug.Log("json saved: " + json);
    }

    public static SettingsData Load()
    {
        if (!PlayerPrefs.HasKey(PrefsKey))
        {
            Debug.Log("no saved settings found, using defaults");
            return null;
        }

        string json = PlayerPrefs.GetString(PrefsKey);
        Debug.Log("json loaded: " + json);
        return JsonUtility.FromJson<SettingsData>(json);
    }

    public static void DeleteSave()
    {
        PlayerPrefs.DeleteKey(PrefsKey);
        PlayerPrefs.Save();
        Debug.Log("save deleted");
    }
}
