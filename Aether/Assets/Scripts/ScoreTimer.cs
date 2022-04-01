using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;


public class ScoreTimer : MonoBehaviour {
    public float currentTime = 0f;
    public float startingTime = 75f;

    public TMP_Text scoreText;
    public TMP_Text distanceText;

	public PlayerMovement pm;
	public float maxDistance;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();

        switch (scene.name) {
            case "Level1":
                startingTime = 50f;
				maxDistance = 580f;
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
        distanceText.text = pm.getCurrentPosition() + "/" + maxDistance + "m";
    }
}
