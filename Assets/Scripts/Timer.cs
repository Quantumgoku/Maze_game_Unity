using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private float bestTime = float.MaxValue;
    private bool isRunning = false;
    void Start()
    {
        startTime = Time.time;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
    }
    public void StartTimer()
    {
        startTime = Time.time;
        isRunning = true;
    }
    public void StopTimer()
    {
        if(IsRunning)
        {
            isRunning = false;
            float elapsedTime = Time.time - startTime;
            if(elapsedTime > bestTime)
            {
                bestTime = elapsedTime;
            }
        }
    }
    public void SetBestTime(float time)
    {
        bestTime = time;
    }
    public bool IsRunning => bestTime == float.MaxValue;
}
