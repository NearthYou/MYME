using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [FormerlySerializedAs("isStart")] [SerializeField] private bool isStageStart = false;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterSpawnTransform;
    
    [Header("Monster Spawn Setting")]
    [SerializeField] float spawnDelayTime = 2f;
    
    private GameObject[] monsterSpawnPoints;
    private float currentTime = 0f;
    private float stageCount;
    
    private void Start()
    {
        currentTime = spawnDelayTime;
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
    }
    
    public void StartStage(float delayTime = 2f)
    {
        spawnDelayTime = delayTime;
        currentTime = spawnDelayTime;
        isStageStart = true;
    }

    private void Update()
    {
        if (isStageStart)
        {
            currentTime -= Time.deltaTime;
            
            if (currentTime <= 0)
            {
                SpawnMonster();
                currentTime = spawnDelayTime;
            }
        }
    }
    
    private void SpawnMonster()
    {
        int randomIndex = Random.Range(0, monsterSpawnPoints.Length);
        var monster = Instantiate(monsterPrefab, monsterSpawnPoints[randomIndex].transform.position, Quaternion.identity, monsterSpawnTransform);
        monster.GetComponent<Monster>().SetDirection((EDirection)randomIndex);
    }
}
