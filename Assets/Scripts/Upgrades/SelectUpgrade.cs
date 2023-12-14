using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TiposDeUpgrade;

public class SelectUpgrade : MonoBehaviour
{
    public UpgradeType upgradeType;
    public GameObject upgradeIndicatorPrefab; // Prefab del indicador visual
    public Material upgradeMaterial; // Material asociado a la mejora

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyUpgrade(other.gameObject);

            // Crea el indicador visual si es necesario
            if (upgradeIndicatorPrefab != null)
            {
                // Crea el indicador visual directamente
                GameObject upgradeIndicator = Instantiate(upgradeIndicatorPrefab, transform.position, Quaternion.identity);

                // Configura el material del indicador visual
                Renderer indicatorRenderer = upgradeIndicator.GetComponent<Renderer>();
                if (indicatorRenderer != null)
                {
                    // Asigna el material directamente
                    indicatorRenderer.material = upgradeMaterial;
                }

                // Configura el padre del indicador visual
                upgradeIndicator.transform.parent = other.transform;
            }

            gameObject.SetActive(false);
        }
    }

    void ApplyUpgrade(GameObject player)
    {
        // Aplicar la mejora al jugador según el tipo
        switch (upgradeType)
        {
            case UpgradeType.Health:
                PlayerHealth.Obj.GiveHeath();
                break;
            case UpgradeType.Bombs:
                BombCount.Obj.UpgradeMoreBombs();
                break;
                // Añadir más tipos de mejoras según sea necesario
        }
        
    }
}