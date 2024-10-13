using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBlink : MonoBehaviour
{
    private bool isStart;
    private bool isBlink = false;
    private float time = 0;
    private float startTime = 0;
    private TMP_Text text;
   
    private int count = 0;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.enabled = false;
    }

    void Update()
    {
        if(isStart)
        {
            startTime += Time.deltaTime;
            if(startTime >= 2)
            {
                isStart = false;
                isBlink = true;
                text.enabled = true;
            }
        }
        
        if (isBlink)
        {
            time += Time.deltaTime;
            if (time >= 0.5f)
            {
                time = 0;
                text.enabled = !text.enabled;
                count++;
            }
        }
        
        if(count >= 5)
        {
            isBlink = false;
            text.enabled = true;
        }
    }
    
    void OnEnable()
    {
        isStart = true;
    }
    
    void OnDisable()
    {
        text.enabled = false;
        isStart = false;
        isBlink = false;
    }
}
