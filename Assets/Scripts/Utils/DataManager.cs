using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 웨이브 별 몬스터 생성 수
    public readonly string[] MonsterAmmount = new string[]{ "7","9","11","13","16","18","20","22","24","25","26","28","29","30","32","33","35","36","38","40"  };
    
    // 웨이브 별 몬스터 생성 간격
    public readonly string[] MonsterDelay = new string[]{ "1.0","0.9","0.81","0.76","0.71","0.69","0.67","0.65","0.63","0.61","0.59","0.57","0.55","0.53","0.51","0.5","0.5","0.5","0.5","0.5"  };
    
    // 웨이브 별 에러 발생 확률
    public readonly string[] ErrorProbality = new string[]
    {
        "0", "0", "0", "0", "0", "0", "0", "30", "30", "70", "30", "30", "30", "30", "70", "30", "30", "40", "40", "100"
    };
    
    // 웨이브 별 에러 종류
    public readonly EErrorType[] ErrorType =
    {
        EErrorType.None , EErrorType.Continuous, EErrorType.None, EErrorType.Error2, EErrorType.None, EErrorType.None, EErrorType.Error3, EErrorType.Random
    };
}