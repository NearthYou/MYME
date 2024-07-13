using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField] private MonsterText monsterText;
    
    [SerializeField] private bool isStageStart = false;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterParent;
    
    private float[] spawnDelayTime;
    private int[] monsterCount;
    private float[] monsterSpeed;
    
    private GameObject[] monsterSpawnPoints;
    
    private float currentTime = 0f;
    private int monCount;
    private int curMonCount;
    private int remainMonCount;
    private float speed;
    
    private void Start()
    {
        Monster.OnMonsterDie += CountUp;
        
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
        
        spawnDelayTime = Managers.Data.MonsterDelay.Select(x=>float.Parse(x)).ToArray();
        monsterCount = Managers.Data.MonsterAmmount.Select(x=>int.Parse(x)).ToArray();
        monsterSpeed = Managers.Data.MonsterSpeed.Select(x=>float.Parse(x)).ToArray();
        
        //NextStage();
    }

    private void OnDestroy()
    {
        Monster.OnMonsterDie -= CountUp;
    }

    public void NextStage()
    {
        Debug.Log("NextStage");
        currentTime = spawnDelayTime[stageCount-1];
        speed = monsterSpeed[stageCount-1];
        monCount = monsterCount[stageCount-1];
        remainMonCount = monCount;
        curMonCount = 0;
        StartCoroutine(WaitMonsterDead(monCount));
        
        stageText.SetText(stageCount);
        monsterText.SetText(monCount);
        
        isStageStart = true;
    }
    
    private void CountUp()
    {
        curMonCount++;
        Debug.Log(curMonCount);
        monsterText.SetText(remainMonCount - curMonCount);
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
                currentTime = spawnDelayTime[stageCount];
            }
            else if (monCount <= 0)
            {
                monCount = 0;
                isStageStart = false;
                stageCount++;
            }
        }
    }

    private IEnumerator WaitMonsterDead(int count)
    {
        yield return new WaitUntil(()=> curMonCount == count);
        
        yield return new WaitForSeconds(5f);
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
