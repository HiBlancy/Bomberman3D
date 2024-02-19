using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblityForUpgrade : MonoBehaviour
{
    public List<GameObject> upgradePrefabs; // Lista de prefabs para diferentes tipos de upgrades.

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        if (collision.collider.CompareTag("Explotion"))
        {
            BlocksOnScreen.Obj.BlockDestroyed();
            Debug.Log("blocsssss");
            
        if (Random.Range(0.0f, 1.0f) > 0.2f)
                GenerateRandomUpgrade();

            Destroy(gameObject);
        }
    }

    private void GenerateRandomUpgrade()
    {
        int randomIndex = Random.Range(0, upgradePrefabs.Count);
        GameObject upgradePrefab = upgradePrefabs[randomIndex];

        _ = Instantiate(upgradePrefab, transform.position, Quaternion.identity);
    }
}