using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUpgrade : MonoBehaviour, IUpgrade
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player.GiveLife();
            Destroy(gameObject);
        }
    }
    public void ApplyUpgrade(PlayerController player)
    {
        player.GiveLife();
    }

}