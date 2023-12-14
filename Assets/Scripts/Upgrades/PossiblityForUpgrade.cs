using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblityForUpgrade : MonoBehaviour
{
    public GameObject upgradePrefab; // Prefab del upgrade a soltar
    public float upgradeDropChance = 0.5f; // Probabilidad de soltar un upgrade (0.0 - 1.0)

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // La caja ha sido rota por el jugador
            Destroy(gameObject);

            // Determinar si se suelta un upgrade
            if (Random.value < upgradeDropChance)
            {
                Instantiate(upgradePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}