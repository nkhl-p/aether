using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTimer : MonoBehaviour {
    public float currentTime = 0f;
    public float startingTime = 75f;

    public TMP_Text scoreText;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name) {
            case "Level1":
                startingTime = 50f;
                break;
            case "Level2":
                startingTime = 50f;
                break;
            default:
                Debug.Log("Code should not reach here!");
                break;
        }
        currentTime = startingTime;
    }

    void Update() {
        currentTime -= 1 * Time.deltaTime;
        scoreText.text = currentTime.ToString("0") + "s";

        if (currentTime < 0) {
            Debug.Log("Player Dead! Timer = 0");
            currentTime = 0;
            // FindObjectOfType<PlayerMovement>().DeathByOutOfTime();
            FindObjectOfType<PlayerMovement>().Die();
        }
    }
}
