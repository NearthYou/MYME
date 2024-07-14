using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOff : MonoBehaviour
{
    private Animator animator;
    
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject image;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Invoke(nameof(ActiveOff), 2f);
    }

    private void ActiveOff()
    {
        image.SetActive(true);
        animator.SetTrigger("Switch");
        Invoke(nameof(OnUI), 4f);
    }
    
    private void OnUI()
    {
        gameoverUI.SetActive(true);
    }
}
