using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerHearts playerHearts;
    private PlayerInput playerInput;
    
    private int hp=3;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        hp = 3;
        playerHearts.SetHearts(hp);
    }

    private void OnDamage()
    {
        if (hp > 1)
        {
            hp--;
            playerHearts.SetHearts(hp);
            //Debug.Log($"Player HP: {hp}");
        }
        else
        {
            playerHearts.SetHearts(0);
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
        else if (other.CompareTag("Bug"))
        {
            playerInput.SetBug(other.GetComponent<IBugControl>());
        }
    }
}