using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    
    [SerializeField] GameObject gameClearUI;
    [SerializeField] Sprite[] gameClearImage;
    [SerializeField] Sprite[] gameClearText;
    
    [SerializeField] Image[] image;

    private void OnEnable()
    {
        Invoke(nameof(Scene1), 5f);
    }

    private void Scene1()
    {
        image[1].sprite = gameClearText[0];
        Invoke(nameof(Scene2), 5f);
    }
    
    private void Scene2()
    {
        image[0].sprite = gameClearImage[0];
        image[1].sprite = gameClearText[1];
        Invoke(nameof(Scene3), 5f);
    }
    
    private void Scene3()
    {
        image[1].sprite = gameClearText[2];
        Invoke(nameof(Scene4), 5f);
    }
    
    private void Scene4()
    {
        image[0].sprite = gameClearImage[1];
        image[1].sprite = gameClearText[3];
        Invoke(nameof(Scene5), 5f);

    }
    
    private void Scene5()
    {
        gameClearUI.SetActive(true);
    }
}
