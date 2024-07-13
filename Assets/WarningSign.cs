using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningSign : MonoBehaviour
{
    private bool isWarning;
    private Image image;
    
    Color initColor;
    private float delay;

    private void Awake()
    {
        image = GetComponent<Image>();
        initColor = image.color;
        delay = 1f;
    }

    public void SetDelay(float _delay)
    {
        delay = _delay;
    }
    
    private void OnEnable()
    {
        StartCoroutine(Warning());
    }

    private void OnDisable()
    {
        StopCoroutine(Warning());
    }

    private IEnumerator Warning()
    {
        isWarning = true;
        while (isWarning)
        {
            
            image.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(delay);
            image.color = initColor;
            yield return new WaitForSeconds(delay);
        }
    }
    
}
