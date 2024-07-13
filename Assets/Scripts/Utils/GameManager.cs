using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private StageController stageController;
    
    private int suspicion = 0;
    public bool isComeback;
    private bool isTimerRunning;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        AddSuspicion(50);
    }

    private void Update()
    {
        if (suspicion > 0 && !isTimerRunning && isComeback)
        {
            if (!SubStractCondition())
            {
                StartCoroutine(SubstractTimer());
            }
        }
        else if (suspicion <= 0 || SubStractCondition() || !isComeback)
        {
            StopCoroutine(SubstractTimer());
        }
    }

    private bool SubStractCondition()
    {
        return player.playerInput.CanMove || stageController.IsBugCountRemain();
    }

    public void AddSuspicion(int value)
    {
        suspicion += value;
        suspicion = Mathf.Clamp(suspicion, 0, 100);
        player.playerUI.SetSuspicion(suspicion);
        Debug.Log("Suspicion: " + suspicion);
        
        if(suspicion >= 100)
        {
            GameOver();
        }
    }
    
    public void ResetSuspicion()
    {
        suspicion = 0;
        player.playerUI.SetSuspicion(suspicion);
    }

    public IEnumerator ComeBackTimer()
    {
        isComeback = false;
        yield return new WaitForSeconds(20f);
        isComeback = true;
    }

    private IEnumerator SubstractTimer()
    {
        isTimerRunning = true;
        yield return new WaitForSeconds(5f);
        AddSuspicion(-10);
        isTimerRunning = false;
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
