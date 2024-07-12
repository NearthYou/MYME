using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [Header("테스트 용")]
    [SerializeField] private int stageCount=1;
    [Space(5)]
    
    [Header("UI")]
    [SerializeField] private StageText stageText;
    
    [SerializeField] private bool isStageStart = false;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterParent;
    
    private float[] spawnDelayTime;
    private int[] monsterCount;
    
    private GameObject[] monsterSpawnPoints;
    private float currentTime = 0f;
    private int curMonCount;
    
    private void Start()
    {
        //currentTime = spawnDelayTime;
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
        
        spawnDelayTime = Managers.Data.MonsterDelay.Select(x=>float.Parse(x)).ToArray();
        monsterCount = Managers.Data.MonsterAmmount.Select(x=>int.Parse(x)).ToArray();
        
        //NextStage();
    }
    
    public void NextStage()
    {
        stageText.SetStageText(stageCount);
        currentTime = spawnDelayTime[stageCount-1];
        curMonCount = monsterCount[stageCount-1];
        isStageStart = true;
    }

    public void StageClear()
    {
        stageCount++;
        isStageStart = false;
    }

    private void Update()
    {
        if (isStageStart)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0 && curMonCount > 0)
            {
                SpawnMonster();
                curMonCount--;
                currentTime = spawnDelayTime[stageCount];
            }
            else if (curMonCount <= 0)
            {
                isStageStart = false;
            }
        }
    }
    
    private void SpawnMonster()
    {
        int randomIndex = Random.Range(0, monsterSpawnPoints.Length);
        var monster = Instantiate(monsterPrefab, monsterSpawnPoints[randomIndex].transform.position, Quaternion.identity, monsterParent);
        monster.GetComponent<Monster>().SetDirection((EDirection)randomIndex);
    }

    public void SkipStageButton()
    {
        isStageStart = false;
        stageCount++;
        
        var monsters = monsterParent.GetComponentsInChildren<Monster>();
        foreach (var monster in monsters)
        {
            monster.Dead();
        }
        
        NextStage();
    }
}
