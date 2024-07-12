using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class UIText : MonoBehaviour
{
    protected TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public abstract void SetText <T>(T value);
}
