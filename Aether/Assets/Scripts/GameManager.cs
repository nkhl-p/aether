using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager inst;
    [SerializeField] GameObject pauseMenu = null;
    [SerializeField] GameObject soundOnIcon = null;
    [SerializeField] GameObject soundOffIcon = null;
    AudioManager audioManagerInstance = null;

    // variables for level loading
    public Animator transition;
    public float transitionTime = 0.25f;


    void Start() {
        inst = this;
        audioManagerInstance = FindObjectOfType<AudioManager>();
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel("MainMenu"));
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadLevel1() {
        StartCoroutine(LoadLevel("Level1"));
    }

    public void LoadLevel2() {
        StartCoroutine(LoadLevel("Level2"));
    }

    public void LoadLevel3() {
        StartCoroutine(LoadLevel("Level3"));
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
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

    IEnumerator LoadLevel(string sceneName) {
        // Play animation
        transition.SetTrigger("Start");


        // Wait for animation to play
        yield return new WaitForSeconds(transitionTime);

        // Load Scene
        SceneManager.LoadScene(sceneName);
    }
    
    public void NextButtonClicked()
    {
        FindObjectOfType<PopUp>().NextButtonClicked();
    }

    public void SkipAllButtonClicked()
    {
        FindObjectOfType<PopUp>().SkipAllButtonClicked();
    }

}
