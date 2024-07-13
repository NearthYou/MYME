using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // 웨이브 별 몬스터 생성 수
    public readonly string[] MonsterAmmount = new string[]
    {
        "7","9","11","13","16","18","20","22","24","25","26","28","29","30","32","33","35","36","38","40","41","42","43","44","45","46","47","48","49","50","51","52","53","54","55","56","57","58","59","60","61","62","63","64","65","66","67","68","69","70"
    };
    
    // 웨이브 별 몬스터 생성 간격
    public readonly string[] MonsterDelay = new string[]
    {
        "1","0.95","0.9","0.855","0.81","0.79","0.77","0.75","0.73","0.71","0.69","0.67","0.65","0.63","0.61","0.6","0.6","0.6","0.6","0.6","0.59","0.58","0.57","0.56","0.55","0.55","0.55","0.55","0.55","0.55","0.54","0.53","0.52","0.51","0.5","0.49","0.48","0.47","0.46","0.45","0.44","0.43","0.42","0.41","0.4","0.4","0.4","0.4","0.4","0.4"
    };
    
    // 웨이브 별 에러 발생 확률
    public readonly string[] ErrorProbality = new string[]
    {
        "0","100","0","100","0","0","100","25","25","60","25","25","25","25","60","25","25","25","25","100","25","25","25","25","60","25","25","25","25","100","25","25","25","25","60","25","25","25","25","100","25","25","25","25","60","25","25","25","25","100"
    };

    public readonly string[] MonsterSpeed = new string[]
    {
        "3","3","3","3","3","3.1","3.2","3.3","3.4","3.5","3.6","3.7","3.8","3.9","4","4","4","4","4","4","4.1","4.1","4.1","4.2","4.2","4.2","4.3","4.3","4.3","4.4","4.4","4.4","4.5","4.5","4.5","4.6","4.6","4.6","4.7","4.7","4.7","4.8","4.8","4.9","4.9","5","5","5","5","5"
    };
    
    // 웨이브 별 에러 종류
    public readonly EErrorType[] ErrorType =
    {
        EErrorType.None,EErrorType.Continuous,EErrorType.None,EErrorType.Error2,EErrorType.None,EErrorType.None,EErrorType.Error3,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random,EErrorType.Random
    };
}