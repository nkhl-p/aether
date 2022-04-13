using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Tutorial : MonoBehaviour {
    
    public static int distance = 0;
    public float currentTime = 0f;
    public float counter = 0f;
    public float startingTime = 75f;
    public TMP_Text scoreText;
    public TMP_Text distanceText;
    public TMP_Text LeftArrowText;
    public TMP_Text RightArrowText;
    public TMP_Text BluePathText;
    public TMP_Text GreenPathText;
    public TMP_Text RedPathText;
    public TMP_Text PowerUpText;
	public PlayerMovement pm;
	public float maxDistance;

    void Start() {
        Scene scene = SceneManager.GetActiveScene();
        switch (scene.name) {
            case "Tutorial":
                startingTime = 50f;
				maxDistance = 300f;
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
        //scoreText.text = currentTime.ToString("0") + "s";

        while (transform.position.z < 100) {
            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //LeftArrowText.text = "Use left arrow key to move left";
                //RightArrowText.text = "Use right arrow key to move right";
                Debug.Log("Arrow Keys!");
            }

            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //LeftArrowText.text = "Use space bar to jump";
                Debug.Log("Space bar");
            }

            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //BluePathText.text = "Blue path is for moderate speed";
                Debug.Log("Blue");
            }

            counter = currentTime + 5 * Time.deltaTime;
            while (currentTime < counter) {
                //GreenPathText.text = "Green path is for high speed";
                //RedPathText.text = "Yellow path is for slow speed";
                Debug.Log("Two paths");
            }
        }

        while (transform.position.z < 200) {
            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //PowerUpText.text = "Collect time capsule for Extra 5 seconds";
                Debug.Log("Power up");
            }

            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //PowerUpText.text = "Collect purple capsule for HUGE spaceship";
            }

        }

        while (transform.position.z < 300) {
            counter = currentTime + 3 * Time.deltaTime;
            while (currentTime < counter) {
                //PowerUpText.text = "Collect green capsule to Levitate";
            }
        }

        if (transform.position.z == 300) {
            //PowerUpText.text = "You have completed the tutorial";
        }

        if (currentTime < 0) {
            Debug.Log("Player Dead! Timer = 0");
            currentTime = 0;
            // FindObjectOfType<PlayerMovement>().DeathByOutOfTime();
            FindObjectOfType<PlayerMovement>().Die();
        }
        distanceText.text = pm.getCurrentPosition() + "/" + maxDistance + "m";
    }
}

