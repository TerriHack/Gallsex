using TMPro;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    private bool timerActive = true;
    private float currentTime;
    public int startMinutes;
    public TextMeshProUGUI currentTimeText;
    
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
