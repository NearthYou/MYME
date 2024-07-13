using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerUI playerUI;
    
    private int suspicion = 0;
    
    public void AddSuspicion(int value)
    {
        suspicion += value;
        suspicion = Mathf.Clamp(suspicion, 0, 100);
        playerUI.SetSuspicion(suspicion);
        Debug.Log("Suspicion: " + suspicion);
    }
}
