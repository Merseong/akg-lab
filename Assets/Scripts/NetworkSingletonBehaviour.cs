using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkSingletonBehaviour<T> : NetworkBehaviour where T : class
{
    public static T Inst;

    protected virtual void Awake()
    {
        Inst = this as T;
    }
}
