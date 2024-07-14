using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private StageText stageText;
    [SerializeField] private MonsterText monsterText;
    [SerializeField] private TMP_Text stateText; 
    [Space(5)]
    
    [SerializeField] private PlayerHearts playerHearts;
    [SerializeField] private Suspicion suspicion;
    [SerializeField] private WarningSign warningSign;
    [SerializeField] private GameObject manual;
    [SerializeField] private GameObject skip;

    private void Start()
    {
        stateText.gameObject.SetActive(false);
        warningSign.gameObject.SetActive(false);
    }

    public void SetStageText(int stage)
    {
        stageText.SetText(stage);
    }
    
    public void SetMonsterText(int monster)
    {
        monsterText.SetText(monster);
    }
    
    public void SetBugText(int bug)
    {
        monsterText.SetBugText(bug);
    }
    
    public void SetHearts(int hp)
    {
        playerHearts.SetHearts(hp);
    }
    
    public void SetSuspicion(int value)
    {
        suspicion.SetGauge(value);
    }
    
    public void ActiveSuspicion(bool isActive)
    {
        suspicion.gameObject.SetActive(isActive);
    }
    
    public void SetStateText(string text)
    {
        stateText.color = Color.black;
        stateText.text = text;
        stateText.gameObject.SetActive(true);
    }
    
    public void SetWarningSign(bool isWarning)
    {
        if (isWarning)
        {
            stateText.color = Color.red;
            SetStateText("Warning!");
        }

        warningSign.gameObject.SetActive(isWarning);
    }

    public void SetWarningSignDelay(float delay = 1f)
    {
        warningSign.SetDelay(delay);
    }
    
    public void SetManual(bool isManual)
    {
        manual.SetActive(isManual);
    }
    
    public void SetSkipButton(bool isSkip)
    {
        skip.SetActive(isSkip);
    }
}
