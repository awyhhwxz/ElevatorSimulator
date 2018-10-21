using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    private Dictionary<string, IUIHandler> _uiDic = new Dictionary<string, IUIHandler>();

    public bool RegistryUI(string name, IUIHandler handler)
    {
        if(!_uiDic.ContainsKey(name))
        {
            _uiDic.Add(name, handler);
            return true;
        }

        return false;
    }

    public void SetVisible(string name, bool visible)
    {
        IUIHandler handler = null;
        if(_uiDic.TryGetValue(name, out handler))
        {
            handler.SetVisible(visible);
        }
    }

    public void SetVisibleAlone(string name)
    {
        if (_uiDic.ContainsKey(name))
        {
            foreach(var pair in _uiDic)
            {
                pair.Value.SetVisible(name == pair.Key);
            }
        }
    }

    public void Clear()
    {
        _uiDic.Clear();
    }
}
