using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSelector<T>{


    protected Dictionary<int, T> _items;
    

    public T Select(int commandType)
    {
        T item = default(T);
        _items.TryGetValue(commandType, out item);

        return item;
    }
}
