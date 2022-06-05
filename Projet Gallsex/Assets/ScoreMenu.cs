using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldTimePillar;
    [SerializeField] private TextMeshProUGUI silverTimePillar;
    [SerializeField] private TextMeshProUGUI bronzeTimePillar;
    
    [SerializeField] private TextMeshProUGUI level1;   
    [SerializeField] private TextMeshProUGUI level2;  
    [SerializeField] private TextMeshProUGUI level3;
    [SerializeField] private TextMeshProUGUI level4;
    
    [SerializeField] private TextMeshProUGUI lastRunTime;

    private float _goldTime;
    private float _silverTime;
    private float _bronzeTime;  
    private float _timeLevel1;
    private float _timeLevel2;
    private float _timeLevel3;
    private float _timeLevel4;
    private float _lastTime;

    private void Start()
    {
        SetTheScore();
    }

    public void SetTheScore()
    {
        _goldTime = PlayerPrefs.GetFloat("goldTime");
        _silverTime = PlayerPrefs.GetFloat("silverTime");
        _bronzeTime = PlayerPrefs.GetFloat("bronzeTime");
        
        _timeLevel1 = PlayerPrefs.GetFloat("bestLevel1Time");
        _timeLevel2 = PlayerPrefs.GetFloat("bestLevel2Time");
        _timeLevel3 = PlayerPrefs.GetFloat("bestLevel3Time");
        _timeLevel4 = PlayerPrefs.GetFloat("bestLevel4Time");
        
        _lastTime = PlayerPrefs.GetFloat("LastRun");

        TimeSpan time1 = TimeSpan.FromSeconds(_goldTime);       
        TimeSpan time2 = TimeSpan.FromSeconds(_silverTime);     
        TimeSpan time3 = TimeSpan.FromSeconds(_bronzeTime);
        
        TimeSpan timeLevel1 = TimeSpan.FromSeconds(_timeLevel1);       
        TimeSpan timeLevel2 = TimeSpan.FromSeconds(_timeLevel2);     
        TimeSpan timeLevel3 = TimeSpan.FromSeconds(_timeLevel3);
        TimeSpan timeLevel4 = TimeSpan.FromSeconds(_timeLevel4);
        
        TimeSpan lastTime = TimeSpan.FromSeconds(_lastTime);

        goldTimePillar.text = time1.ToString(@"mm\:ss\:fff");
        silverTimePillar.text = time2.ToString(@"mm\:ss\:fff");
        bronzeTimePillar.text = time3.ToString(@"mm\:ss\:fff");
        
        level1.text = timeLevel1.ToString(@"mm\:ss\:fff");
        level2.text = timeLevel2.ToString(@"mm\:ss\:fff");
        level3.text = timeLevel3.ToString(@"mm\:ss\:fff");
        level4.text = timeLevel4.ToString(@"mm\:ss\:fff");
        
        lastRunTime.text = lastTime.ToString(@"mm\:ss\:fff");
        
        
    }
}
