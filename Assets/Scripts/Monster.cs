using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;

    private GameObject player;
    private EDirection direction;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        gameObject.tag = "Monster";
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position =
                Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
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
}