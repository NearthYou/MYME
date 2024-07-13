using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    [SerializeField] private StageController stageController;
    [SerializeField] private GameObject playerUI;
    
    [Header("Production")]
    [SerializeField] private GameObject mainScene;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private GameObject popupScene;
    [SerializeField] private List<Hand> hands = new List<Hand>();
    private int suspicion = 0;
    public bool isComeback;
    private bool isTimerRunning;

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

    public void GameSetting()
    {
        mainScene.SetActive(false);
        player.gameObject.SetActive(true);
        player.playerInput.CanPressed = true;
        player.playerInput.isTutorial = true;
        player.isTutorial = true;
        Invoke(nameof(TutorialStart), 3f);
    }
    
    public void UIOn()
    {
        playerUI.SetActive(true);
        player.playerUI.SetSkipButton(true);

    }

    public void TutorialStart()
    {
        stageController.TutorialStart();
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

    public IEnumerator NextStory()
    {
        player.playerInput.CanPressed = false;
        yield return new WaitForSeconds(3f);

        player.playerUI.SetSkipButton(false);
        player.playerUI.SetManual(false);
        playerUI.SetActive(false);

        loadingScene.SetActive(true);
        
        yield return new WaitForSeconds(5f);
        
        // 손 퇴장
        hands.ForEach(hand => hand.MoveOut());
        
        yield return new WaitForSeconds(3f);
        
        loadingScene.SetActive(false);
        playerUI.SetActive(true);
        
        yield return new WaitForSeconds(3f);

        // 캐릭터 뒤돌게함
        // 캐릭터 앞에 팝업창
        yield return StartCoroutine(player.playerInput.StoryMove());
        
        popupScene.SetActive(true);

        yield return StartCoroutine(popupScene.transform.GetChild(0).GetComponent<PopupScene>().StoryProduction());
        popupScene.SetActive(false);
        player.playerInput.popup.SetActive(false);
        
        yield return new WaitForSeconds(2f);
        
        hands.ForEach(hand => hand.MoveIn());
        yield return new WaitForSeconds(1f);
        
        player.playerUI.ActiveSuspicion(true);
        
        yield return new WaitForSeconds(3f);
        
        player.playerInput.CanPressed = true;
        player.playerInput.isTutorial = false;
        player.isTutorial = false;
        stageController.isTutorial = false;
        stageController.NextStage();
    }

    public void SkipTutorial()
    {
        player.playerInput.CanPressed = true;
        player.playerInput.isTutorial = false;
        player.isTutorial = false;
        stageController.isTutorial = false;
        player.playerUI.SetManual(false);
        player.playerUI.ActiveSuspicion(true);

        stageController.SkipTutorial();
    }
}