using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class ScoreTimer : MonoBehaviour {
    public float currentTime = 0f;
    public float startingTime = 75f;

    public TMP_Text scoreText;
    public TMP_Text timerText;
	public float levitationPowerupsTimer = 0f;
    public TMP_Text distanceText;
    public TMP_Text obstacleCounterText;
    public int obstacleCounter = 0;
    public PlayerMovement pm;
	public float maxDistance;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
		timerText.enabled = false;
        switch (scene.name) {
            case "Level1":
                startingTime = 50f;
				maxDistance = 600f;
                break;
            case "Level2":
                startingTime = 50f;
				maxDistance = 695f;
                break;
            default:
                Debug.Log("Code should not reach here!");
                break;
        }
        currentTime = startingTime;
		pm = FindObjectOfType<PlayerMovement>();
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
		distanceText.text = pm.getCurrentPosition() + "/" + maxDistance + "m";
    }

	public void startLevitationTimer(float powerUpApplicableDuration) {
		levitationPowerupsTimer = powerUpApplicableDuration;
		timerText.enabled = true;
	}

	public void stopLevitationTimer() {
		levitationPowerupsTimer = 0f;
		timerText.enabled = false;
	}

	public void IncrementDistroyedObstacleCounter()
	{
		obstacleCounter += 1;
		obstacleCounterText.text = obstacleCounter.ToString("0");
	}
}
