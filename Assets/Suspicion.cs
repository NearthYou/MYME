using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Suspicion : MonoBehaviour
{
    [SerializeField] private GameObject[] gaugeBars;
    [SerializeField] private Sprite[] eyeSprites; 
    [SerializeField] private Image eyeSprite;
    
    public void SetGauge(int suspicion)
    {
        int barCount = suspicion/gaugeBars.Length;
        
        for (int i = 0; i < gaugeBars.Length; i++)
        {
            if (i < barCount)
            {
                gaugeBars[i].SetActive(true);
            }
            else
            {
                gaugeBars[i].SetActive(false);
            }
        }
        
        var count = GetEyeSpriteCount(suspicion);
        
        if(count >= 6)
        {
            count = 5;
        }
        
        eyeSprite.sprite = eyeSprites[count];
    }
    
    private int GetEyeSpriteCount(int suspicion)
    {
        return 6 * suspicion / 100;
    }
}
