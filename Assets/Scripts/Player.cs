using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    
    public PlayerInput playerInput;
    public PlayerUI playerUI;
    
    private int hp = 3;

    private void Start()
    {
        PlayerInput.OnMoveEvent += ChangeCamera;
        playerInput = GetComponent<PlayerInput>();
        hp = 3;
        playerUI.SetHearts(hp);
    }

    private void OnDestroy()
    {
        PlayerInput.OnMoveEvent -= ChangeCamera;
    }

    private void OnDamage()
    {
        if (hp > 1)
        {
            hp--;
            playerUI.SetHearts(hp);
            Debug.Log($"Player HP: {hp}");
        }
        else
        {
            playerUI.SetHearts(0);
            GameManager.instance.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Skeleton>().Dead();
            OnDamage();
        }
        
        if (other.CompareTag("Bug"))
        {
            OnDamage();
            //playerInput.SetBug(other.GetComponent<IBugControl>());
        }
        
        if (other.CompareTag("InitPos"))
        {
            playerInput.SetIsStandingInit(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InitPos"))
        {
            playerInput.SetIsStandingInit(false);
        }
    }

    private void ChangeCamera(bool isDefense)
    {
        playerCamera.SetCamera(isDefense);
    }
}