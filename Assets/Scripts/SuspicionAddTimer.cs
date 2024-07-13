using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionAddTimer : MonoBehaviour
{
    private float time;
    private int amount;
    private bool isRunning;
    
    public void SetTimer(float _time, int _amount)
    {
        time = _time;
        amount = _amount;
        isRunning  = false;
    }

    public void StartTimer()
    {
        if(isRunning)
            return;
        
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        StopCoroutine(Timer());
    }
    
    public IEnumerator Timer()
    {
        isRunning = true;
        yield return new WaitForSeconds(time);
        GameManager.instance.AddSuspicion(amount);
        isRunning  = false;
    }
}