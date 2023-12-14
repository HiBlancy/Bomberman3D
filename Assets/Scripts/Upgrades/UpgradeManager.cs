using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TiposDeUpgrade;

public class UpgradeManager : MonoBehaviour
{
    public Material[] upgradeMaterials; // Materiales asociados a las mejoras disponibles

    void Start()
    {
        // Selecciona aleatoriamente el tipo de mejora
        UpgradeType randomUpgradeType = (UpgradeType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(UpgradeType)).Length);

        // Configura el material del indicador visual
        Renderer indicatorRenderer = GetComponent<Renderer>();
        if (indicatorRenderer != null)
        {
            // Asigna el material directamente
            indicatorRenderer.material = GetMaterialForUpgrade(randomUpgradeType);
        }
    }

    Material GetMaterialForUpgrade(UpgradeType upgradeType)
    {
        // Devuelve el material asociado al tipo de mejora
        return upgradeMaterials[(int)upgradeType];
    }
}