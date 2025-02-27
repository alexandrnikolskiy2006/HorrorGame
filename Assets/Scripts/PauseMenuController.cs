using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenuController : MonoBehaviour
{
    public bool pauseGame = false;
    public GameObject pauseGameMenu;

    // public Texture2D cursorTexture;

    void Start()
    {
        pauseGameMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGameMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void PauseOff()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
}
