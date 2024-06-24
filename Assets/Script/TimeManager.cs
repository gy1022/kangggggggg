using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    const int MIN_TIME_VALUE = 0;
    const int MAX_TIME_VALUE = 86400; // 24 * 60 * 60

    public Slider timer;
    public Text timeText;

    public Light sunLight;
    private Vector3 initAngle;

    public string startZero(int num)
    {
        return (num < 10) ? "0" + num : "" + num;
    }

    public void valueChanged(Slider slider, Text text)
    {
        int diff = MAX_TIME_VALUE - MIN_TIME_VALUE;
        int value = MIN_TIME_VALUE + (int)(diff * slider.value); // 0 ~ 1ÀÇ °ª

        string h, m, s;
        int hh = value / 3600;
        int mm = (value % 3600) / 60;
        int ss = (value % 60);

        h = startZero(hh);
        m = startZero(mm);
        s = startZero(ss);

        text.text = "Time" + " " + h + " : " + m + " : " + s;

        sunLight.transform.rotation
            = Quaternion.Euler(slider.value * 360.0f + 270.0f, initAngle.y, initAngle.z);
    }

    void Start()
    {
        initAngle = sunLight.transform.localEulerAngles;

        timer.onValueChanged.AddListener(delegate { valueChanged(timer, timeText); });

        DateTime dt = DateTime.Now;
        int HH = Int32.Parse(dt.ToString("HH"));
        int mm = Int32.Parse(dt.ToString("mm"));
        int ss = Int32.Parse(dt.ToString("ss"));

        timer.value = (float)(HH * 3600 + mm * 60 + ss) / MAX_TIME_VALUE;
    }
}