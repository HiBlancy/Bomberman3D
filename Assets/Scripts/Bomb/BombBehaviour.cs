using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public GameObject[] explotion;

    public void SetUpBomb(Vector3 startPosition)
    {
        this.transform.position = startPosition;
        gameObject.SetActive(true);
        StartCoroutine(Explotion());
        StartCoroutine(Desactive());

        explotion = new GameObject[3];
        explotion = GameObject.FindGameObjectsWithTag("Explotion");
    }


    IEnumerator Explotion()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("bom");

        foreach(GameObject exp in explotion)
        {
            exp.SetActive(true);
        }

    }

    IEnumerator Desactive()
    {
        yield return new WaitForSeconds(5);
        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        PlayerInstBomb.Obj.bombsOnScreen--;
    }
}