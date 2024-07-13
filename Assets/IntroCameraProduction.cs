using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class IntroCameraProduction : MonoBehaviour
{
    private CinemachineStoryboard storyboard;
    private bool fadeIn;
    private bool coolTime;
    private float time;
    private bool fadeOut;

    private void Awake()
    {
        storyboard = GetComponent<CinemachineStoryboard>();
    }

    private void Update()
    {
        if (fadeIn)
        {
            time += Time.deltaTime;
            storyboard.m_Alpha = Mathf.Lerp(0, 1, time);
            if (time >= 1)
            {
                fadeIn = false;
                coolTime = true;
                time = 0;
                GameManager.instance.GameSetting();
            }
        }
        else if (coolTime)
        {
            time += Time.deltaTime;
            if (time >= 2)
            {
                coolTime = false;
                fadeOut = true;
                time = 0;
            }
        }
        else if (fadeOut)
        {
            time += Time.deltaTime;
            storyboard.m_Alpha = Mathf.Lerp(1, 0, time);

            if (time >= 1)
            {
                fadeOut = false;
                time = 0;
            }
            
            GameManager.instance.UIOn();
        }
    }

    public void FadeIn()
    {
        fadeIn = true;
    }
}