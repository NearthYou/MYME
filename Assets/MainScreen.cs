using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour
{
    [SerializeField] IntroCameraProduction introCameraProduction;
    [SerializeField] Hand[] hands;
    [SerializeField] SoundManager soundManager;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            soundManager.PlaySFX("MainTouch");
            introCameraProduction.FadeIn();
            for (int i = 0; i < hands.Length; i++)
            {
                hands[i].MoveIn();
            }
            gameObject.SetActive(false);
        }
    }
}