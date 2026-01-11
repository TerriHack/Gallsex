using TMPro;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class prefabTimer : MonoBehaviour
{

    private bool timerActive = true;
    public float currentTime;
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
}
