using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithBomb : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Explotion"))
        {
            Debug.Log("jugador");
            //porque detecta multiples veces
        }
    }
}
