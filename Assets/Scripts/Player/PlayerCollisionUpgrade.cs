using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionUpgrade : MonoBehaviour
{
    [SerializeField] AudioSource audioClip;

    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("powerup"))
        {
            audioClip.Play();
        }     
    }
}