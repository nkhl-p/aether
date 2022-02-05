
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 10f;
    
    public TMP_Text scoreText;

    // Start is called before the first frame update

    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        //scoreText1.text = currentTime.ToString("0");
        //scoreText = scoreText.GetComponent<Text>().Text;
        Debug.Log(currentTime.ToString("0"));
        scoreText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
