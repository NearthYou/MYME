using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 웨이브 별 몬스터 생성 수
    public readonly int[] MonsterAmmount = new int[]
    {
        7,9,11,13,16,18,20,22,24,25,26,28,29,30,32,33,35,36,38,40,41,42,43,44,45,46,47,48,49,50
    };
    
    // 웨이브 별 몬스터 생성 간격
    public readonly float[] MonsterDelay = new float[]
    {
        1.0f, 0.95f, 0.9f, 0.855f, 0.81f, 0.79f, 0.77f, 0.75f, 0.73f, 0.71f, 0.69f, 0.67f, 0.65f, 0.63f, 0.61f, 
        0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.59f, 0.58f, 0.57f, 0.56f, 0.55f, 0.55f, 0.55f, 0.55f, 0.55f, 0.55f
    };


    public readonly float[] MonsterSpeed = new float[]
    {
        3.0f, 3.0f, 3.0f, 3.0f, 3.0f, 3.1f, 3.2f, 3.3f, 3.4f, 3.5f, 3.6f, 3.7f, 3.8f, 3.9f, 4.0f, 4.0f, 
        4.0f, 4.0f, 4.0f, 4.0f, 4.1f, 4.1f, 4.1f, 4.2f, 4.2f, 4.2f, 4.3f, 4.3f, 4.3f, 4.4f
    };
    
    // 웨이브 별 에러 발생 확률
    public readonly int[] ErrorProbality = new int[]
    {
        100,0,0,100,0,0,100,25,25,60,25,25,25,25,60,25,25,25,25,100,25,25,25,25,60,25,25,25,25,100
    };

    public readonly int[] FairyProbability = new int[]
    {
        8,8,8,8,8,8,8,4,4,4,4,4,4,4,4,4,4,4,4,4,3,3,3,3,3,3,3,3,3,3
    };
}