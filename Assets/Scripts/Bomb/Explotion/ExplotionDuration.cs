using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{

    Player player;

    private void Awake()
    {
        player = GameObject.Find("First Person Controller").GetComponent<Player>();
    }

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
            player.LoseHealth();
    }
}