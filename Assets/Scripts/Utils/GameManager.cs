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
        player.playerUI.SetWarningSign(true);
        yield return new WaitForSeconds(10f);
        player.playerUI.SetWarningSignDelay(0.5f);
        yield return new WaitForSeconds(10f);
        player.playerUI.SetWarningSign(false);
        player.playerUI.SetWarningSignDelay();
        isComeback = true;
    }

    private IEnumerator SubstractTimer()
    {
        isTimerRunning = true;
        yield return new WaitForSeconds(5f);
        AddSuspicion(-10);
        isTimerRunning = false;
    }

    public void LoadingText()
    {
        player.playerUI.SetStateText("Loading...");
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        player.playerInput.CanPressed = false;
        // 키보드 입력 막기
        // 결과창 UI 띄우기 (다시 하기, 게임 종료)
    }

    public void GameStart()
    {
        Debug.Log("Game Clear");
    }

    public void GameClear()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
        player.playerInput.CanPressed = false;
        Debug.Log("Game Clear");
        // 결과창 UI 띄우기 (다시 하기, 게임 종료)
    }

    private void SettingPanel()
    {
        Time.timeScale = 0;
        player.playerInput.CanPressed = false;
    }
}
