using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{
    float startTime;
    TextMeshProUGUI timerText;

    void Start()
    {
        startTime = Time.time;
        timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.time - startTime;

        int minute = ((int)time / 60);
        int seconds = ((int)time % 60);

        string additionalZero1;
        string additionalZero2;
        
        additionalZero1 = minute < 10 ? "0" : "";
        additionalZero2 = seconds < 10 ? "0" : "";

        timerText.text = additionalZero1 + minute + ":" + additionalZero2 + seconds;
    }
}
