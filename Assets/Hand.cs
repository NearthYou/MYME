using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] private Vector2 targetPos;
    RectTransform rectTransform;
    private Vector2 initPos;
    private bool isMovingIn;
    private bool isMovingOut;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = targetPos;
    }
    
    void Update()
    {
        if (isMovingIn)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, initPos, Time.deltaTime * 5);
            if (Vector3.Distance(rectTransform.position, targetPos) < 0.1f)
            {
                isMovingIn = false;
            }
        }

        if (isMovingOut)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, targetPos, Time.deltaTime * 5);
            if (Vector3.Distance(rectTransform.position, initPos) < 0.1f)
            {
                isMovingOut = false;
            }
        }
    }
    
    public void MoveIn()
    {
        isMovingIn = true;
        isMovingOut = false;
    }

    public void MoveOut()
    {
        isMovingOut = true;
        isMovingIn = false;
    }
}


