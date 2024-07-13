using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCamera playerCamera;
    
    public PlayerInput playerInput;
    public PlayerUI playerUI;
    private SpriteRenderer spriteRenderer;

    private int hp = 3;
    private bool isHit;
    public bool isTutorial;
    
    private void Start()
    {
        PlayerInput.OnMoveEvent += ChangeCamera;
        playerInput = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = 3;
        playerUI.SetHearts(hp);
    }

    private void OnDestroy()
    {
        PlayerInput.OnMoveEvent -= ChangeCamera;
    }

    private void OnDamage()
    {
        if (isTutorial)
            return;
        
        if (isHit)
            return;
        
        isHit = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
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
        Invoke("OffDamage", 1f);
    }

    private void OnHeal()
    {
        hp++;
        
        if (hp > 3)
            hp = 3;
        playerUI.SetHearts(hp);
        Debug.Log($"Player HP: {hp}");
    }
    
    private void OffDamage()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Skeleton>().Suicide();
            OnDamage();
        }
        
        if (other.CompareTag("Fairy"))
        {
            other.GetComponent<Fairy>().Suicide();
            OnHeal();
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