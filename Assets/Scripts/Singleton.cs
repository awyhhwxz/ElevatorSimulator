using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : new()
{

    protected Singleton()
    {
    }

    public static readonly T Instance = new T();



    public virtual void Initialize()
    {

    }
}
