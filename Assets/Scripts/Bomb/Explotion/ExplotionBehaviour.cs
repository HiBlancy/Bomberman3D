using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Block"))
        {
            Destroy(collision.gameObject);
        }
    }
}