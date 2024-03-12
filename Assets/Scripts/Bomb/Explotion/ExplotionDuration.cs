using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableElement", 4f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Explotion")
        collision.gameObject.GetComponent<PossiblityForUpgrade>().enabled = true;
    }

    void DisableElement()
    {
        Destroy(gameObject);
    }
}