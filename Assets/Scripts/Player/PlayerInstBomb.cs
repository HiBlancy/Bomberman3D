using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstBomb : MonoBehaviour
{
    public GameObject bombaPrefab;
    //public int radioExplosionActual;
    [SerializeField] Transform playerPosition;

    [SerializeField] AudioSource audioClip;

    Player player;
    public int bombsOnScreen;

    public bool canPuMoreBombs = true;

    public static PlayerInstBomb Obj { get; private set; }

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;

        audioClip = GetComponent<AudioSource>();

        player = GameObject.Find("First Person Controller").GetComponent<Player>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canPuMoreBombs)
        {
            PlantarBomba();
            BombsOnScreen();
        }
    }

    void PlantarBomba()
    {
        audioClip.Play();
        GameObject bomb = PoolManager.Obj.BombPool.GetElement();

        Bomba bombBehaviour = bomb.GetComponent<Bomba>();
        bombBehaviour.SummonBomb(playerPosition.position);

        bomb.GetComponent<Bomba>().explosion_power = player.explosion_power;

        if (player.kickBomb)
            bomb.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void BombsOnScreen()
    {
        bombsOnScreen++;

        //si hay el mismo numero de bombas que no se pueda sacar mas
        if (player.bombs == bombsOnScreen) //como hacer para que me detecte que no puede poner mas bombas
            canPuMoreBombs = false;
    }

    public void BombExploded()
    {
        bombsOnScreen--;

        if (player.bombs != bombsOnScreen)
            canPuMoreBombs = true;
    }
}