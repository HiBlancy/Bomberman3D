using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionBehaviour : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("si");
            Destroy(collision.gameObject);
        }
    }
}