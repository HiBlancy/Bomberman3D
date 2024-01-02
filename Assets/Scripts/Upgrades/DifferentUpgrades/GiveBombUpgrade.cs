using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBombUpgrade : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Obj.GiveBomb();
            Destroy(gameObject);
        }
    }
}