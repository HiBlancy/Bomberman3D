using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void GiveLife()
    {
        PlayerHealth.Obj.GiveHeath();
    }

    public void GiveBomb()
    {
        BombCount.Obj.UpgradeMoreBombs();
    }
}