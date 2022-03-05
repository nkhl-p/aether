using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public static GameManager inst;
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject soundOnIcon = null;
    [SerializeField] GameObject soundOffIcon = null;
    AudioManager audioManagerInstance = null;


    void Start() {
        inst = this;
        audioManagerInstance = FindObjectOfType<AudioManager>();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame() {
        SceneManager.LoadScene(4);
    }

    public void StartLevel1() {
        SceneManager.LoadScene(1);
    }

    public void StartLevel2() {
        SceneManager.LoadScene(3);
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
    }

    public void HelpMenu() {
        SceneManager.LoadScene(5);
    }

    public void MuteSound() {
        FindObjectOfType<AudioManager>().StopPlaying(SoundEnums.THEME.GetString());
        soundOnIcon.SetActive(false);
        soundOffIcon.SetActive(true);
    }

    public void UnmuteSound() {
        FindObjectOfType<AudioManager>().Play(SoundEnums.THEME.GetString());
        soundOffIcon.SetActive(false);
        soundOnIcon.SetActive(true);
    }

}
