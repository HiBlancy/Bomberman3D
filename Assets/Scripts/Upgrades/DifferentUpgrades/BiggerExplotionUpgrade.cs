using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerExplotionUpgrade : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Obj.BiggerExplotion();
            Destroy(gameObject);
        }
    }
}
