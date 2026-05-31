using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private string mainMenuScene = "MainMenuScene";

    private bool gamePaused = false;

    [SerializeField] Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
        player.DisableControls();
        player.CrosshairController.ShowCursor();
    }

    private void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        player.EnableControls();
        player.CrosshairController.HideCursor();
    }

    public void OnResume()
    {
        ResumeGame();
    }

    public void OnSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            pauseMenuPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Pause Menu Settings not working!");
        }
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}
