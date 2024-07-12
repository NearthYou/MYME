using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    private RectTransform _rectTrans; 
    
    private void OnEnable()
    {
        Debug.Log("Call");
        Debug.Log(Screen.safeArea);
    }
}
