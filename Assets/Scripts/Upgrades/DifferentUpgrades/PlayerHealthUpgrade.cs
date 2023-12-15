using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUpgrade : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.Obj.GiveHeath();
            Destroy(gameObject);
        }
    }
}