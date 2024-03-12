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

    public void HurtSound()
    {
        audioClip.Play();
    }
}