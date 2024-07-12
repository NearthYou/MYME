using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField] private bool isStart = false;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Transform monsterSpawnTransform;
    private GameObject[] monsterSpawnPoints;
    
    [Header("Monster Spawn Setting")]
    [SerializeField] float spawnDelayTime = 2f;
    float currentTime = 0f;
    // Update is called once per frame

    private void Start()
    {
        currentTime = spawnDelayTime;
        monsterSpawnPoints = new GameObject[monsterSpawnPoint.transform.childCount];
        for (int i = 0; i < monsterSpawnPoint.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnPoint.transform.GetChild(i).gameObject;
        }
    }

    private void Update()
    {
        if (isStart)
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
