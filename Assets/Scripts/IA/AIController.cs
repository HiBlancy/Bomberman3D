using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float speed = 5f; 

    private Rigidbody rb; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Inicia el movimiento aleatorio
        InvokeRepeating("RandomMovement", 0f, 3f); // Llama a RandomMovement cada 3 segundos
    }


    void RandomMovement()
    {
        // Genera una dirección de movimiento aleatoria
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        // Mueve la IA en la dirección aleatoria
        rb.velocity = randomDirection * speed;
    }
}