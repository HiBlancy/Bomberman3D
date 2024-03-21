using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Obj { get; private set; }

    public Pool BombPool => _bombPool;
    public Pool ExplotionPool => _explotionPool;

    [SerializeField] Pool _bombPool;
    [SerializeField] Pool _explotionPool;

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }
}