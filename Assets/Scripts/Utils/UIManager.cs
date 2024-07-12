using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private List<UIWindow> _windowDataList;
    private Stack<UIWindow> _activeWindowStack;

    public void Init()
    {
        _windowDataList = new List<UIWindow>();
        _activeWindowStack = new Stack<UIWindow>();
    }

    #region Window Data
    
    public void AddWindow(UIWindow window)
    {
        if (window == null || ContainWindow(window))
            return;

        _windowDataList?.Add(window);
        window.Hide();
    }

    public bool ContainWindow(UIWindow window)
    {
        return _windowDataList.Contains(window);
    }
    
    public T GetWindow<T>() where T : UIWindow
    {
        return _windowDataList.Find(x => x.GetType() == typeof(T)) as T;
    }
    
    #endregion
    
    #region Active Window

    public T ShowWindow<T>() where T : UIWindow
    {
        var window = GetWindow<T>();
        _activeWindowStack.Push(window);
        
        window.Open();

        return window;
    }
    
    public T PeakWindow<T>() where T : UIWindow
    {
        return _activeWindowStack?.Peek() as T;
    }

    public void CloseWindow()
    {
        if (_activeWindowStack.Count > 0)
        {
            var window = _activeWindowStack?.Pop();
            window.Hide();
        }
    }

    #endregion

    public void Quit()
    {
        _windowDataList = null;
        _activeWindowStack = null;
    }
}
