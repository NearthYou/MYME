using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateText : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(ActiveOff),3f);
    }

    private void ActiveOff()
    {
        gameObject.SetActive(false);
    }
}
