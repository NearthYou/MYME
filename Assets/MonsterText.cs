using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterText : UIText
{
    public override void SetText<T>(T value)
    {
        text.text = $"남은 몬스터 수 : {value}";
    }
}
