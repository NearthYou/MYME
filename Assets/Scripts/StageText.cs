using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageText : UIText
{
    public override void SetText<T>(T value)
    {
        text.text = $"현재 스테이지 : {value}";
    }
}
