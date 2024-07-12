using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHearts : MonoBehaviour
{
    private GameObject[] hearts;

    private void Start()
    {
        hearts = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts[i] = transform.GetChild(i).gameObject;
        }
    }

    public void SetHearts(int value)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < value)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }
}
