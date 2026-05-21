using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GoBackCreditManager : MonoBehaviour
{
    [SerializeField] private string mainMenuScene = "MainMenuScene";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //private void BackToMainMenu()
    //{
    //    SceneManager.LoadScene(mainMenuScene);
    //}

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            //BackToMainMenu();
            SceneManager.LoadScene(mainMenuScene);
        }
    }
}
