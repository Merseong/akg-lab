using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : class
{
    public static T Inst;

    protected virtual void Awake()
    {
        Inst = this as T;
    }
}
