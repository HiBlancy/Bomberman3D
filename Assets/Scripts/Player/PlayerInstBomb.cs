using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstBomb : MonoBehaviour
{
    public GameObject bombaPrefab;
    public int radioExplosionActual = 1;
    [SerializeField] Transform playerPosition;
    public static PlayerInstBomb Obj { get; private set; }

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlantarBomba();
            BombCount.Obj.BombsOnScreen();
        }
    }

    void PlantarBomba()
    {
        GameObject bomb = PoolManager.Obj.BombPool.GetElement();

        Bomba bombBehaviour = bomb.GetComponent<Bomba>();
        bombBehaviour.SummonBomb(playerPosition.position);

        bomb.GetComponent<Bomba>().radioExplosion = radioExplosionActual;
    }

    public void MejorarExplosion()
    {
        radioExplosionActual = radioExplosionActual + 10;
    }
}