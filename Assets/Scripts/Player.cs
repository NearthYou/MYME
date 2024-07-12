using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput playerInput;
    private float hp;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        hp = 1000;
    }

    private void OnDamage()
    {
        if (hp > 1)
        {
            //hp--;
            //Debug.Log($"Player HP: {hp}");
        }
        else
        {
            Debug.Log("Game Over");
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Dead();
            OnDamage();
        }
    }
}