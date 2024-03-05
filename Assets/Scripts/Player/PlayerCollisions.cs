using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    [SerializeField] AudioSource audioClip;

    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Explotion"))
        {
            Debug.Log("jugador");
            //porque detecta multiples veces
        }
        if (collision.CompareTag("powerup"))
        {
            audioClip.Play();
        }     
    }
}