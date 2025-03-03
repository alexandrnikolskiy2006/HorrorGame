using UnityEngine;
// using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenuController: MonoBehaviour
{
    // public Button startGameButton;
    // public string newButtonText = "Продолжить";

    // private void Start()
    // {
    //     var buttonText = startGameButton.GetComponentInChildren<Text>();
    //     startGameButton.onClick.AddListener(() => ChangeButtonText(buttonText));
    // }

    public void StartGame()
    {
        PauseMenuScript.isPaused = false;
        SceneManager.LoadScene("TestScene");
    }

    public void ExitGame()
    {
        // Debug.Log("Выход из игры...");
        Application.Quit();
    }

    // private void ChangeButtonText(Text text)
    // {
    //     if (text != null)
    //     {
    //         text.text = newButtonText;
    //     }
    // }
}