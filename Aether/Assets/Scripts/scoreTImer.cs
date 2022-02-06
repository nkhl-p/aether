using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreTimer : MonoBehaviour {
    float currentTime = 0f;
    float startingTime = 10f;

    public TMP_Text scoreText;
    // Start is called before the first frame update

    void Start() {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update() {
        currentTime -= 1 * Time.deltaTime;
        //scoreText.text = currentTime.ToString("0");
        scoreText.GetComponent<Text>().text = currentTime.ToString("0");

        if (currentTime <= 0) {
            currentTime = 0;
        }
    }
}
