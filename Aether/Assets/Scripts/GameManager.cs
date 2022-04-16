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
    public float transitionTime = 1f;


    void Start() {
        inst = this;
        audioManagerInstance = FindObjectOfType<AudioManager>();
    }

    public void MainMenu() {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(0));
        //SceneManager.LoadScene(0);
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
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EndGame() {
        StartCoroutine(LoadLevel(7));
        //SceneManager.LoadScene(7);
    }

    public void LevelInstructionsMenu() {
        StartCoroutine(LoadLevel(5));
        //SceneManager.LoadScene(5);
    }

    public void StartLevel1() {
        StartCoroutine(LoadLevel(2));
        //SceneManager.LoadScene(2);
    }

    public void NextInstructionLevel1() {
        StartCoroutine(LoadLevel(6));
        //SceneManager.LoadScene(6);
    }

    public void StartLevel2() {
        StartCoroutine(LoadLevel(4));
        //SceneManager.LoadScene(4);
    }

    public void NextInstructionLevel2() {
        StartCoroutine(LoadLevel(3));
        //SceneManager.LoadScene(3);
    }

    public void CloseApplicationOnMainMenuExit() {
        Debug.Log("Quitting the application");
        Application.Quit();
    }

    public void InstructionsMenu() {
        StartCoroutine(LoadLevel(1));
        //SceneManager.LoadScene(1);
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

    IEnumerator LoadLevel(int levelIndex) {
        // Play animation
        transition.SetTrigger("Start");


        // Wait for animation to play
        yield return new WaitForSeconds(transitionTime);

        // Load Scene
        SceneManager.LoadScene(levelIndex);
    }

}
