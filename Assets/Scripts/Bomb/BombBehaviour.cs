using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] GameObject explotion;

    public void SetUpBomb(Vector3 startPosition)
    {
        this.transform.position = startPosition;
        gameObject.SetActive(true);
        StartCoroutine(Explotion());
        StartCoroutine(Desactive());
    }

    IEnumerator Explotion()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("bom");
        explotion.SetActive(true);

        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator Desactive()
    {
        yield return new WaitForSeconds(5);
        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        PlayerInstBomb.Obj.bombsOnScreen--;
        explotion.SetActive(false);
    }
}