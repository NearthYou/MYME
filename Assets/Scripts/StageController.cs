using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StageController : MonoBehaviour
{
    [Header("테스트 용")]
    [SerializeField] private bool isStageStart = false;
    [Space(5)]
    
    [Header("UI")]
    [SerializeField] private PlayerUI playerUI;

    [Header("Monster")]
    [SerializeField] private GameObject[] monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterParent;
    
    [Header("Manager")]
    [SerializeField] private BugSpawnManager bugSpawnManager;
    
    private int stageCount;
    
    private GameObject[] monsterSpawnPoints;
    private SuspicionAddTimer suspicionAddTimer;
    private List<GameObject> monsterList;
    
    private float speed;
    private float currentTime = 0f;
    
    private int monCount;
    private int curMonCount;
    private int remainMonCount;
    private int bugCount;
    private int randomBugCount;
    private int bugProbability;
    private int fairyProbability;
    public bool isTutorial;
    
    private void Start()
    {
        monsterList = new List<GameObject>();
        suspicionAddTimer = Utils.TryOrAddComponent<SuspicionAddTimer>(gameObject);
        suspicionAddTimer.SetTimer(1,10);
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
        
        bugCount = 0;
        //NextStage();
    }

    public void StopTimer()
    {
        suspicionAddTimer.StopTimer();
    }

    public void NextStage()
    {
        if(stageCount >= 10)
        {
            GameManager.instance.GameClear();
            suspicionAddTimer.StopTimer();
            return;
        }
        
        if (monsterList.Count > 0)
        {
            monsterList.Clear();
        }
        
        Debug.Log("NextStage");
        currentTime = Managers.Data.MonsterDelay[stageCount];
        speed = Managers.Data.MonsterSpeed[stageCount];
        monCount = Managers.Data.MonsterAmmount[stageCount];
        bugProbability = Managers.Data.ErrorProbality[stageCount];
        fairyProbability = Managers.Data.FairyProbability[stageCount];
        remainMonCount = monCount;
        curMonCount = 0;
        StartCoroutine(WaitStageClear(monCount));
        
        playerUI.SetStageText((stageCount+1).ToString());
        playerUI.SetMonsterText(monCount);
        
        isStageStart = true;
    }
    
    public void TutorialStart()
    {
        Debug.Log("TutorialStart");
        currentTime = Managers.Data.MonsterDelay[stageCount];
        speed = Managers.Data.MonsterSpeed[stageCount];
        monCount = Managers.Data.MonsterAmmount[stageCount];
        bugProbability = Managers.Data.ErrorProbality[stageCount];
        fairyProbability = Managers.Data.FairyProbability[stageCount];
        remainMonCount = monCount;
        curMonCount = 0;

        playerUI.SetStageText("???");
        playerUI.SetMonsterText(monCount);
        
        StartCoroutine(WaitStageClear(monCount));
        
        isTutorial = true;
        isStageStart = true;
    }
    
    public void CountDeadMonster()
    {
        if (remainMonCount - curMonCount <= 0)
            return;
        
        curMonCount++;
        playerUI.SetMonsterText(remainMonCount - curMonCount);
    }
    
    public void CountDeleteBug()
    {
        if (bugCount >= randomBugCount)
        {
            bugCount = 0;
            randomBugCount = 0;
            return;
        }
        bugCount++;
    }

    private void Update()
    {
        if (isStageStart)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0 && monCount > 0)
            {
                SpawnMonster();
                monCount--;
                currentTime = Managers.Data.MonsterDelay[stageCount];
            }
            else if (monCount <= 0)
            {
                monCount = 0;
                isStageStart = false;
            }
        }

        if (IsBugCountRemain() && GameManager.instance.isComeback)
        {
            suspicionAddTimer.StartTimer();
        }
    }

    private IEnumerator WaitStageClear(int count)
    {
        yield return new WaitUntil(()=> curMonCount == count);

        if (isTutorial)
        {
            StartCoroutine(GameManager.instance.NextStory());
            yield break;
        }
        
        if (Utils.GetRandom(bugProbability))
        {
            // Error UI
            randomBugCount = Random.Range(3, 7);
            bugSpawnManager.SpawnBug(randomBugCount);
            //playerUI.SetBugText(randomBugCount);
            yield return StartCoroutine(GameManager.instance.ComeBackTimer());
            SoundManager.instance.PlaySFX("ErrorResolved");
        }
        
        // Loading UI
        GameManager.instance.LoadingText();
        yield return new WaitForSeconds(3f);
        
        stageCount++;
        NextStage();
    }
    
    public bool IsBugCountRemain()
    {
        return bugCount != 0;
    }
    
    private void SpawnMonster()
    {
        GameObject monsterPrefabs = Utils.GetRandom(fairyProbability) ? monsterPrefab[1] : monsterPrefab[0];
        
        int randomIndex = Random.Range(0, monsterSpawnPoints.Length);
        var monster = Instantiate(monsterPrefabs, monsterSpawnPoints[randomIndex].transform.position,
            Quaternion.identity, monsterParent);
        monster.GetComponent<ISpeed>().SetSpeed(speed); 
        
        monsterList.Add(monster);
    }

    public void SkipTutorial()
    {
        StopCoroutine(nameof(WaitStageClear));
        isTutorial = false;
        isStageStart = false;
        
        if(monsterList.Count>0)
            monsterList.ForEach(Destroy);
        
        stageCount = 0;
        NextStage();
    }
    
    public void DeleteAllMonster()
    {
        if(monsterList.Count>0)
        {
            monsterList.ForEach(Destroy);
            monsterList.Clear();
        }
    }
    
}
