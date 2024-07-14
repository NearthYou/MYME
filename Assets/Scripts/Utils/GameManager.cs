using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
    
    [SerializeField] private Player player;
    [SerializeField] private StageController stageController;
    [SerializeField] private GameObject playerUI;
    
    [Header("Production")]
    [SerializeField] private GameObject mainScene;
    [SerializeField] private GameObject loadingScene;
    [SerializeField] private GameObject popupScene;
    [SerializeField] private List<Hand> hands = new List<Hand>();
    
    [SerializeField] private GameObject gameoverUI_HP;
    [SerializeField] private GameObject gameoverUI_Gauge;
    [SerializeField] private GameObject gameClearUI;
    private int suspicion = 0;
    public bool isComeback;
    private bool isTimerRunning;
    private bool isGameOver;

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

    private void Start()
    {
        SoundManager.instance.PlayBGM("MainBGM");
        SetResolution();
        
    }
    
    public void SetResolution()
    {
        int setWidth = 1920; // 화면 너비
        int setHeight = 1080; // 화면 높이

        //해상도를 설정값에 따라 변경
        //3번째 파라미터는 풀스크린 모드를 설정 > true : 풀스크린, false : 창모드
        Screen.SetResolution(setWidth, setHeight, true);
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
            GameOver_Gauge();
        }
    }
    
    public void ResetSuspicion()
    {
        suspicion = 0;
        player.playerUI.SetSuspicion(suspicion);
    }

    public IEnumerator ComeBackTimer()
    {
        HandMoveOut();
        isComeback = false;
        player.playerUI.SetWarningSign(true);
        yield return new WaitForSeconds(10f);
        player.playerUI.SetWarningSignDelay(0.5f);
        yield return new WaitForSeconds(10f);
        player.playerUI.SetWarningSign(false);
        player.playerUI.SetWarningSignDelay();
        HandMoveIn();
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
    
    public void GameOver_HP()
    {
        if (isGameOver)
            return;
        
        Debug.Log("Game Over");
        //Time.timeScale = 0;
        isGameOver = true;
        player.playerInput.CanPressed = false;
        player.playerInput.CanMove = false;
        player.transform.position = Vector3.zero;
        stageController.DeleteAllMonster();
        playerUI.SetActive(false);
        gameoverUI_HP.SetActive(true);
        
        // 결과창 UI 띄우기 (다시 하기, 게임 종료)
    }
    
    public void GameOver_Gauge()
    {
        if (isGameOver)
            return;
        
        isGameOver = true;
        Debug.Log("Game Over");
        //Time.timeScale = 0;
        player.playerInput.CanPressed = false;
        player.playerInput.CanMove = false;
        player.transform.position = Vector3.zero;

        playerUI.SetActive(false);
        stageController.DeleteAllMonster();

    
        gameoverUI_Gauge.SetActive(true);
        // 결과창 UI 띄우기 (다시 하기, 게임 종료)
    }
    
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameSetting()
    {
        mainScene.SetActive(false);
        player.gameObject.SetActive(true);
        player.playerInput.CanPressed = true;
        player.playerInput.isTutorial = true;
        player.isTutorial = true;
        Invoke(nameof(TutorialStart), 8f);
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
        if (isGameOver)
            return;
        
        Debug.Log("Game Clear");
        isGameOver = true;
        player.playerInput.CanPressed = false;
        player.playerInput.CanMove = false;
        player.transform.position = Vector3.zero;
        stageController.DeleteAllMonster();
        playerUI.SetActive(false);
        gameClearUI.SetActive(true);
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
    
    public void HandMoveIn()
    {
        hands.ForEach(hand => hand.MoveIn());
    }
    
    public void HandMoveOut()
    {
        hands.ForEach(hand => hand.MoveOut());
    }
    
    public void ActiveLoadingScene()
    {
        playerUI.gameObject.SetActive(false);
        loadingScene.SetActive(true);
        Invoke(nameof(OffLoadingScene), 3f);
    }
    
    private void OffLoadingScene()
    {
        loadingScene.SetActive(false);
        playerUI.gameObject.SetActive(true);
    }
}