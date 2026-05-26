using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string startGameScene = "TESTStartGameScene";
    [SerializeField] private string tutorialScene = "TESTTutorialScene";
    [SerializeField] private string creditsScene = "TESTCreditsScene";

    [Header("Panels")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject menuPanel;


    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnStartGame()
    {
        SceneManager.LoadScene(startGameScene);
    }

    public void OnTutorial()
    {
        SceneManager.LoadScene(tutorialScene);
    }

    public void OnSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            menuPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Main Menu Settings not working!");
        }
    }

    public void OnCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
