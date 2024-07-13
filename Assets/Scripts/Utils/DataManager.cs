using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 웨이브 별 몬스터 생성 수
    public readonly int[] MonsterAmmount = new int[]
    {
        7,9,11,13,16,18,20,22,24,25,26,28,29,30,32,33,35,36,38,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,62,63,64,65,66,67,68,69,70
    };
    
    // 웨이브 별 몬스터 생성 간격
    public readonly float[] MonsterDelay = new float[]
    {
        1,0.95f,0.9f,0.855f,0.81f,0.79f,0.77f,0.75f,0.73f,0.71f,0.69f,0.67f,0.65f,0.63f,0.61f,0.6f,0.6f,0.6f,0.6f,0.6f,0.59f,0.58f,0.57f,0.56f,0.55f,0.55f,0.55f,0.55f,0.55f,0.55f,0.54f,
        0.53f,0.52f,0.51f,0.5f,0.49f,0.48f,0.47f,0.46f,0.45f,0.44f,0.43f,0.42f,0.41f,0.4f,0.4f,0.4f,0.4f,0.4f,0.4f
    };


    public readonly float[] MonsterSpeed = new float[]
    {
        3,3,3,3,3,3.1f,3.2f,3.3f,3.4f,3.5f,3.6f,3.7f,3.8f,3.9f,4,4,4,4,4,4,4.1f,4.1f,4.1f,4.2f,4.2f,4.2f,4.3f,4.3f,4.3f,4.4f,4.4f,4.4f,4.5f,4.5f,4.5f,4.6f,4.6f,4.6f,4.7f,4.7f,4.7f,4.8f,4.8f,4.9f,4.9f,5,5,5,5,5
    };
    
    // 웨이브 별 에러 발생 확률
    public readonly int[] ErrorProbality = new int[]
    {
        0,100,0,100,0,0,100,25,25,60,25,25,25,25,60,25,25,25,25,100,25,25,25,25,60,25,25,25,25,100,25,25,25,25,60,25,25,25,25,100,25,25,25,25,60,25,25,25,25,100
    };
    
    // 웨이브 별 에러 종류
    public readonly EErrorType[] ErrorType =
    {
        EErrorType.None,EErrorType.Continuous,EErrorType.None,EErrorType.Error2,EErrorType.None,EErrorType.None,EErrorType.Error3,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,
        EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random
    };
}