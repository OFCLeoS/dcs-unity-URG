using System.IO;
using UnityEngine;

/// <summary>
/// Class which is always instantiated at the start of the game and handles loading different requirements
/// </summary>
static class Bootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        //Debug.LogWarning("FIX BOOTSTRAP!!!");
        // LoadEssentialRessources();
        // CreateBaseDirectories();
        // CreateBaseFiles();
    }

    private static void LoadEssentialRessources()
    {
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("EventSystem")));
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("Essentials")));
    }

    private static void CreateBaseDirectories()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Player Preferences"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Player Preferences");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/Player Data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Player Data");
        }
    }

    // private static void CreateBaseFiles()
    // {
    // IDataService saveLoadSystem = new JSONDataService();

    // string filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.CONTROLS_PREFERENCES);
    // if (!File.Exists(filePath))
    // {
    //     //Default Settings
    //     ControlsPreferences controlsPreferences = new ControlsPreferences(15);
    //     saveLoadSystem.SaveData<ControlsPreferences>(DataPaths.GetRelativeDataPath(DataTypes.CONTROLS_PREFERENCES), controlsPreferences, false);
    // }


    // filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.VIDEO_PREFERENCES);
    // if (!File.Exists(filePath))
    // {
    //     VideoPreferences videoPreferences = new VideoPreferences(Screen.resolutions[Screen.resolutions.Length - 1]);
    //     saveLoadSystem.SaveData<VideoPreferences>(DataPaths.GetRelativeDataPath(DataTypes.VIDEO_PREFERENCES), videoPreferences, false);
    // }

    /*filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.VOLUME_PREFERENCES);
    if (!File.Exists(filePath))
    {
        VolumePreferencesData volumePreferencesData = new VolumePreferencesData();
        saveLoadSystem.SaveData<VolumePreferencesData>(DataPaths.GetRelativeDataPath(DataTypes.VOLUME_PREFERENCES), volumePreferencesData, false);
    }

    filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.LOADOUT);
    if (!File.Exists(filePath))
    {
        LoadoutData loadoutData = new LoadoutData();
        saveLoadSystem.SaveData<LoadoutData>(DataPaths.GetRelativeDataPath(DataTypes.LOADOUT), loadoutData, true);
    }*/

    //     filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.PLAYER_ASSETS);
    //     if (!File.Exists(filePath))
    //     {
    //         PlayerAssets playerAssets = new PlayerAssets();
    //         saveLoadSystem.SaveData<PlayerAssets>(DataPaths.GetRelativeDataPath(DataTypes.PLAYER_ASSETS), playerAssets, true);
    //     }
    //     filePath = Application.persistentDataPath + DataPaths.GetRelativeDataPath(DataTypes.PLAYER_STATS);
    //     if (!File.Exists(filePath))
    //     {
    //         PlayerStats playerStats = new PlayerStats();
    //         saveLoadSystem.SaveData<PlayerStats>(DataPaths.GetRelativeDataPath(DataTypes.PLAYER_STATS), playerStats, true);
    //     }
    // }

    /*private static void LoadVideoSettings()
    {
        IDataService saveLoadSystem = new JSONDataService();
        VideoPreferencesData userVideoPreferences = saveLoadSystem.LoadData<VideoPreferencesData>(DataPaths.GetRelativeDataPath(DataTypes.VIDEO_PREFERENCES), false);
        Screen.SetResolution(
            Screen.resolutions[Screen.resolutions.Length - 1 - userVideoPreferences.resolutionIndex].width,
            Screen.resolutions[Screen.resolutions.Length - 1 - userVideoPreferences.resolutionIndex].height,
            (FullScreenMode)userVideoPreferences.windowMode
            );
    }*/
}
