using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearts : MonoBehaviour
{
    private List<GameObject> hearts;

    private void Start()
    {
        hearts = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SetHearts(int value)
    {
        if(hearts == null)
            return;
        
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < value)
            {
                hearts[i]?.SetActive(true);
            }
            else
            {
                hearts[i]?.SetActive(false);
            }
        }
    }
}