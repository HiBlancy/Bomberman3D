using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBombUpgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BombCount.Obj.UpgradeMoreBombs();
            Destroy(gameObject);
        }
    }
}