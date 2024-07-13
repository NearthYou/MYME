using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveHand
{
    public void MoveIn();
}

public class Hand : MonoBehaviour, IMoveHand
{
    [SerializeField] private Vector2 targetPos;
    RectTransform rectTransform;
    private Vector2 initPos;
    private bool isMoving;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initPos = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = targetPos;
    }
    
    void Update()
    {
        if (isMoving)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, initPos, Time.deltaTime * 5);
            if (Vector3.Distance(rectTransform.position, targetPos) < 0.1f)
            {
                isMoving = false;
            }
        }
    }
    
    public void MoveIn()
    {
        isMoving = true;
    }
}


