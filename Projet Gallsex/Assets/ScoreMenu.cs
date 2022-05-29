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

    private float _goldTime;
    private float _silverTime;
    private float _bronzeTime;

    private void Start()
    {
        _goldTime = PlayerPrefs.GetFloat("goldTime");
        _silverTime = PlayerPrefs.GetFloat("silverTime");
        _bronzeTime = PlayerPrefs.GetFloat("bronzeTime");

        TimeSpan time1 = TimeSpan.FromSeconds(_goldTime);       
        TimeSpan time2 = TimeSpan.FromSeconds(_silverTime);     
        TimeSpan time3 = TimeSpan.FromSeconds(_bronzeTime);
        
        goldTimePillar.text = time1.ToString(@"mm\:ss\:fff");
        silverTimePillar.text = time2.ToString(@"mm\:ss\:fff");
        bronzeTimePillar.text = time3.ToString(@"mm\:ss\:fff");
    }
}
