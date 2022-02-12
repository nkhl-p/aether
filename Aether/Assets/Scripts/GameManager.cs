using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager inst;
    [SerializeField] GameObject pauseMenu;

    // Start is called before the first frame update
    void Start() {
        inst = this;
    }

    public void PlayGame() {
        // Todo Update the index after integration
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //SceneManager.LoadScene(2);
    }

    public void ResumeGame() {
        // Todo Update the index after integration
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        //SceneManager.LoadScene(1);
    }

    public void NextLevel() {
        // Todo Update the index after integration
        SceneManager.LoadScene(4);
    }

    public void EndGame() {
        // Todo Update the index after integration
        SceneManager.LoadScene(4);
    }

    public void StartLevel1() {
        // Todo Update the index after integration
        SceneManager.LoadScene(1);
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
    }

    public void HelpMenu() {
        Debug.Log("Opening the help menu");
        SceneManager.LoadScene(5);
    }

}
