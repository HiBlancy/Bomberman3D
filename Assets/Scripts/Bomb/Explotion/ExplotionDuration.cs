using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableElement", 4f);
    }

    void DisableElement()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            { Debug.Log("player hit explostion"); }
    }
}