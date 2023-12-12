using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstBomb : MonoBehaviour
{
    public static PlayerInstBomb Obj { get; private set; }

    int initialBoombs = 2;
    public int bombsOnScreen;
    //AudioSource audioSource;
    [SerializeField] Transform playerPosition;

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q) && (bombsOnScreen != initialBoombs))
        {
            Debug.Log("q");
            PlaceBomb();
            bombsOnScreen++;
        }
    }

    void PlaceBomb()
    {
        GameObject bomb = PoolManager.Obj.BombPool.GetElement();

        BombBehaviour bombBehaviour = bomb.GetComponent<BombBehaviour>();
        bombBehaviour.SetUpBomb(playerPosition.position);
    }
}