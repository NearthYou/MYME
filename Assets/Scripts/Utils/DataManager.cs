using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 웨이브 별 몬스터 생성 수
    public readonly int[] MonsterAmmount = new int[]
    {
        7,9,12,15,20,25,30,35,40,50
    };
    
    // 웨이브 별 몬스터 생성 간격
    public readonly float[] MonsterDelay = new float[]
    {
        1.0f, 0.9f, 0.855f, 0.71f, 0.69f, 0.6f,
        0.58f, 0.57f, 0.56f, 0.54f
    };


    public readonly float[] MonsterSpeed = new float[]
    {
        3.0f, 3.0f, 3.3f, 3.5f, 3.6f, 3.7f, 4.2f, 
        4.3f, 4.45f, 4.6f
    };
    
    // 웨이브 별 에러 발생 확률
    public readonly int[] ErrorProbality = new int[]
    {
        100,0,25,25,50,50,50,60,60,60};

    public readonly int[] FairyProbability = new int[]
    {
        8,8,8,8,4,4,4,4,3,3
    };
}