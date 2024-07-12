using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWindow : UIBase
{
    [SerializeField]
    protected bool _alwaysOpen = false;
    
    protected override bool Init()
    {
        if (!base.Init())
            return false;
        return true;
    }

    protected virtual void Start()
    {
        base.Start();
        Managers.UI.AddWindow(this);
    }
    
    public void Open()
    {
        Setting();
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (_alwaysOpen)
            return;

        Clear();
        gameObject.SetActive(false);
    }

    protected virtual void Setting()
    {
        
    }

    protected virtual void Clear()
    {
        
    }
}