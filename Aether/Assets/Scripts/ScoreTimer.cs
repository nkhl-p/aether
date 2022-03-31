using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreTimer : MonoBehaviour {
    public float currentTime = 0f;
    public float startingTime = 75f;

    public TMP_Text scoreText;
    public TMP_Text timerText;
	public float levitationPowerupsTimer = 0f;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
		timerText.enabled = false;
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

		if (levitationPowerupsTimer > 0 && timerText.enabled == true) { 
			levitationPowerupsTimer -= 1 * Time.deltaTime;
			timerText.text = levitationPowerupsTimer.ToString("0") + "s";
		}

		if (levitationPowerupsTimer < 0 && timerText.enabled == true) {
			stopLevitationTimer();
		}
    }

	public void startLevitationTimer(float powerUpApplicableDuration) {
		levitationPowerupsTimer = powerUpApplicableDuration;
		timerText.enabled = true;
	}

	public void stopLevitationTimer() {
		levitationPowerupsTimer = 0f;
		timerText.enabled = false;
	}
}
