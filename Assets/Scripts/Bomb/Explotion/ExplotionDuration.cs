using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{
    [SerializeField] AudioSource audioClip;

    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        audioClip.Play();
        Invoke("DisableElement", 4f);
    }

    void DisableElement()
    {
        Destroy(gameObject);
    }
}
