using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public static event Action OnMonsterDie;
    
    private float moveSpeed;
    private GameObject target;
    private EDirection direction;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        gameObject.tag = "Monster";
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("InitPos");
    }
    
    void Update()
    {
        if (target != null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(EDirection _direction)
    {
        direction = _direction;
        
        switch (direction)
        {
            case EDirection.East:
                spr.flipX = false;
                break;
            case EDirection.West:
                spr.flipX = true;
                break;
            case EDirection.South:
                spr.flipY = false;
                break;
            case EDirection.North:
                spr.flipY = true;
                break;
        }
    }
    
    public void Dead()
    {
        OnMonsterDie?.Invoke();
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}