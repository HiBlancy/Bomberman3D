using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCount : MonoBehaviour
{
    public static BombCount Obj { get; private set; }

    public int initialBoombs;
    public int bombsOnScreen;
    //AudioSource audioSource;

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    void Start()
    {
        initialBoombs = 2;
    }

    public void UpgradeMoreBombs()
    {
        initialBoombs++;
    }

    public void BombsOnScreen()
    {
        bombsOnScreen++;
    }
}