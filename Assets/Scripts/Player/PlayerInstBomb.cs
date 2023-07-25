using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstBomb : MonoBehaviour
{
    bool ableToPlaceBomb;
    //AudioSource audioSource;
    [SerializeField] Transform playerPosition;

    void Awake()
    {
        ableToPlaceBomb = true;
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q) & ableToPlaceBomb)
        {
            PlaceBomb();
            //ableToPlaceBomb = false;
            //StartCoroutine(waitToPlaceBombAgain());
        }
    }

    void PlaceBomb()
    {
        GameObject bomb = PoolManager.Obj.BombPool.GetElement();

        BombBehaviour bombBehaviour = bomb.GetComponent<BombBehaviour>(); //mirar si sobresale de la pool para que no pete
        bombBehaviour.SetUpBomb(playerPosition.position);
    }

    //IEnumerator waitToPlaceBombAgain()
    //{
    //    yield return new WaitForSeconds(1);
    //    ableToPlaceBomb = true;
    //}
}