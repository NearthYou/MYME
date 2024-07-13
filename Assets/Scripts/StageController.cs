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
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterParent;
    
    [Header("Manager")]
    [SerializeField] private BugSpawnManager bugSpawnManager;
    
    private int stageCount=1;
    
    private GameObject[] monsterSpawnPoints;
    
    private float speed;
    private float currentTime = 0f;
    
    private int monCount;
    private int curMonCount;
    private int remainMonCount;
    private int bugCount;
    
    private void Start()
    {
        Monster.OnMonsterDie += CountDeadMonster;
        BugHole.OnBugDie += CountDeleteBug;
        
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
        
        //NextStage();
    }

    private void OnDestroy()
    {
        Monster.OnMonsterDie -= CountDeadMonster;
        BugHole.OnBugDie -= CountDeleteBug;
    }

    public void NextStage()
    {
        Debug.Log("NextStage");
        currentTime = Managers.Data.MonsterDelay[stageCount];
        speed = Managers.Data.MonsterSpeed[stageCount];
        monCount = Managers.Data.MonsterAmmount[stageCount];
        remainMonCount = monCount;
        curMonCount = 0;
        bugCount = 0;
        StartCoroutine(WaitStageClear(monCount));
        
        playerUI.SetStageText(stageCount+1);
        playerUI.SetMonsterText(monCount);
        
        isStageStart = true;
    }
    
    private void CountDeadMonster()
    {
        curMonCount++;
        Debug.Log(curMonCount);
        playerUI.SetMonsterText(remainMonCount - curMonCount);
    }
    
    private void CountDeleteBug()
    {
        bugCount++;
        Debug.Log(bugCount);
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
    }

    private IEnumerator WaitStageClear(int count)
    {
        yield return new WaitUntil(()=> curMonCount == count);

        var randomBugCount = Random.Range(3, 7);
        bugSpawnManager.SpawnBug(randomBugCount);
        
        yield return new WaitUntil(()=> bugCount == randomBugCount);
        
        yield return new WaitForSeconds(3f);
        
        stageCount++;
        NextStage();
    }
    
    private void SpawnMonster()
    {
        int randomIndex = Random.Range(0, monsterSpawnPoints.Length);
        var monster = Instantiate(monsterPrefab, monsterSpawnPoints[randomIndex].transform.position,
            Quaternion.identity, monsterParent).GetComponent<Monster>();
        monster.SetDirection((EDirection)randomIndex);
        monster.SetSpeed(speed); 
    }
    
}
