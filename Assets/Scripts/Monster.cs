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
    private Animator animator;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameObject.tag = "Monster";
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
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
                animator.SetTrigger("Right");
                break;
            case EDirection.West:
                animator.SetTrigger("Left");
                break;
            case EDirection.South:
                animator.SetTrigger("Behind");
                break;
            case EDirection.North:
                animator.SetTrigger("Front");
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