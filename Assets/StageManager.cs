using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StageManager : MonoBehaviour
{
    [SerializeField] private bool isStart = false;
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject monsterSpawnParent;
    private GameObject[] monsterSpawnPoints;
    
    [Header("Monster Spawn Setting")]
    [SerializeField] float spawnDelayTime = 2f;
    float currentTime = 0f;
    // Update is called once per frame

    private void Start()
    {
        currentTime = spawnDelayTime;
        monsterSpawnPoints = new GameObject[monsterSpawnParent.transform.childCount];
        for (int i = 0; i < monsterSpawnParent.transform.childCount; i++)
        {
            monsterSpawnPoints[i] = monsterSpawnParent.transform.GetChild(i).gameObject;
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
        Instantiate(monster, monsterSpawnPoints[randomIndex].transform.position, Quaternion.identity);
    }
}
