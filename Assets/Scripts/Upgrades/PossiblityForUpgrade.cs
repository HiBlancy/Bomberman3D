using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblityForUpgrade : MonoBehaviour
{
    public List<GameObject> upgradePrefabs; // Lista de prefabs para diferentes tipos de upgrades.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //la caja se destruye si entra en contacto el jugador
        {
            BlocksOnScreen.Obj.BlockDestroyed();
            GenerateRandomUpgrade();
            Destroy(gameObject);
        }
    }

    private void GenerateRandomUpgrade()
    {
        int randomIndex = Random.Range(0, upgradePrefabs.Count);
        GameObject upgradePrefab = upgradePrefabs[randomIndex];

        _ = Instantiate(upgradePrefab, transform.position, Quaternion.identity);

        // IUpgrade upgrade = Instantiate(upgradePrefab, transform.position, Quaternion.identity)?.GetComponent<IUpgrade>();
    }
}