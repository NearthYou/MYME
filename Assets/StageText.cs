using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageText : MonoBehaviour
{
    private TMP_Text stageTMP;
    
    private void Awake()
    {
        stageTMP = GetComponent<TMP_Text>();
    }

    public void SetStageText(int num)
    {
        stageTMP.text = $"현재 스테이지 : {num}";
    }
}
