using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{
    public void ActivateExplotion(Vector3 startPosition)
    {
        gameObject.SetActive(true);
        this.transform.position = startPosition;
        Invoke("DisableElement", 3f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blocks")
        collision.gameObject.GetComponent<PossiblityForUpgrade>().enabled = true;
    }

    void DisableElement()
    {
        PoolManager.Obj.ExplotionPool.ReturnElement(this.gameObject);
    }
}