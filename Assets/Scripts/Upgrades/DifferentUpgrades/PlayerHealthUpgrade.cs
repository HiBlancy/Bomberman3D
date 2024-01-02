using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUpgrade : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Obj.GiveLife();
            Destroy(gameObject);
        }
    }
}