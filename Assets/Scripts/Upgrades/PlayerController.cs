using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Obj { get; private set; }

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    public void GiveLife()
    {
        PlayerHealth.Obj.GiveHeath();
    }

    public void GiveBomb()
    {
        BombCount.Obj.UpgradeMoreBombs();
    }

    public void BiggerExplotion()
    {
        PlayerInstBomb.Obj.MejorarExplosion();
    }
}