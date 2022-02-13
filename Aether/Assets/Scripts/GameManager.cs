using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager inst;
    [SerializeField] GameObject pauseMenu = null;

    void Start() {
        inst = this;
    }

    public void PlayGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void NextLevel() {
        SceneManager.LoadScene(4);
    }

    public void EndGame() {
        SceneManager.LoadScene(4);
    }

    public void StartLevel1() {
        SceneManager.LoadScene(1);
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
    }

    public void HelpMenu() {
        SceneManager.LoadScene(5);
    }

}
