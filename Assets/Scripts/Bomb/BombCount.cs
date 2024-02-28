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
        initialBoombs = 1;
    }

    public void UpgradeMoreBombs()
    {
        initialBoombs++;
        Debug.Log("bombas mas");
    }

    public void BombsOnScreen()
    {
        //si hay el mismo numero de bombas que no se pueda sacar mas
        bombsOnScreen++;
    }
}