using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BugSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject bugPrefab;

    [SerializeField] private PolygonCollider2D mainCollider;
    [SerializeField] private BoxCollider2D excludeCollider;
    
    private Bounds excludeBounds;
    private int bugCount;

    private void Start()
    {
        excludeBounds = excludeCollider.bounds;
        bugCount = 0;
    }

    public void SpawnBug(int num)
    {
        for (int i = 0; i < num; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
        
            var bug = Instantiate(bugPrefab, randomPosition, Quaternion.identity);
            bug.GetComponent<BugHole>().SetArrow(Instantiate(arrowPrefab, Vector2.zero, Quaternion.identity));
        }
    }
    
    private Vector3 GetRandomPosition()
    {
        if (mainCollider == null)
        {
            Debug.LogError("Main collider is not assigned.");
            return Vector3.zero;
        }

        // 특정 콜라이더 범위의 경계 계산
        Bounds mainBounds = mainCollider.bounds;

        Vector3 randomPosition = Vector3.zero;
        bool positionFound = false;
        int maxAttempts = 10; // 무한 루프 방지를 위한 최대 시도 횟수 설정

        for (int i = 0; i < maxAttempts; i++)
        {
            // 특정 콜라이더 범위에서 랜덤 위치 추출
            randomPosition = new Vector3(
                Random.Range(mainBounds.min.x, mainBounds.max.x),
                Random.Range(mainBounds.min.y, mainBounds.max.y),
                0f
            );

            // 제외할 콜라이더 범위에 포함되지 않는지 확인
            if (excludeCollider == null || !excludeBounds.Contains(randomPosition))
            {
                positionFound = true;
                break;
            }
        }

        if (!positionFound)
        {
            Debug.LogWarning("Failed to find a valid random position after " + maxAttempts + " attempts.");
        }

        return randomPosition;
    }
}