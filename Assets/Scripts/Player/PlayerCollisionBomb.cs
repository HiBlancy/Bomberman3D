using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionBomb : MonoBehaviour
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
            audioClip.Play();
            //porque detecta multiples veces
        }
    }
}