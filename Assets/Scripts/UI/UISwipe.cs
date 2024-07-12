using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public abstract class UISwipe : UIBase
{
    protected enum Buttons
    {
        AfterButton,
        BeforeButton
    }

    protected enum Texts
    {
        SwipeValue
    }

    protected int count;
    protected List<string> data;
    public UnityEvent<int> onValueChanged;

    protected override bool Init()
    {
        if (!base.Init())
            return false;
        
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton((int)Buttons.AfterButton).onClick.AddListener(OnClickAfterButton);
        GetButton((int)Buttons.BeforeButton).onClick.AddListener(OnClickBeforeButton);
        
        RegisterData();

        GetText((int)Texts.SwipeValue).text = data[0];
        count = 0;
        
        return true;
    }

    protected virtual void OnClickAfterButton()
    {
        if (count + 1 >= data.Count)
            return;

        count += 1;
        
        RefreshUI();
    }

    protected virtual void OnClickBeforeButton()
    {
        if (count - 1 < 0)
            return;

        count -= 1;
        
        RefreshUI();
    }

    protected virtual void RefreshUI()
    {
        onValueChanged?.Invoke(count);
        GetText((int)Texts.SwipeValue).text = data[count];
    }

    protected virtual void RegisterData() {}

    public string GetData()
    {
        return data[count];
    }
}
