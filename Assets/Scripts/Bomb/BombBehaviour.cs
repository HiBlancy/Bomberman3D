using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    public void SetUpBomb(Vector3 startPosition)
    {
        this.transform.position = startPosition;
        gameObject.SetActive(true);
        StartCoroutine(Desactive());
    }

    IEnumerator Desactive()
    {
        yield return new WaitForSeconds(3);
        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        PlayerInstBomb.Obj.bombsOnScreen--; //When the bomb explodes, you can spawn more
    }
}