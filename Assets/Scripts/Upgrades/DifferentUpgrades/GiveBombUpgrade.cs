using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBombUpgrade : MonoBehaviour , IUpgrade
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //BombCount.Obj.UpgradeMoreBombs();
            Destroy(gameObject);
        }
    }
    public void ApplyUpgrade(PlayerController player)
    {
        player.GiveBomb();
    }
}