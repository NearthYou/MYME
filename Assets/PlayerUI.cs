using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private StageText stageText;
    [SerializeField] private MonsterText monsterText;
    
    [Header("Heart")]
    [SerializeField] private PlayerHearts playerHearts;
    
    public void SetStageText(int stage)
    {
        stageText.SetText(stage);
    }
    
    public void SetMonsterText(int monster)
    {
        monsterText.SetText(monster);
    }
    
    public void SetHearts(int hp)
    {
        playerHearts.SetHearts(hp);
    }
}
