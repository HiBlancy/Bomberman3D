using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblityForUpgrade : MonoBehaviour
{
    public GameObject powerup_prefab;

    void Start()
    {
        powerup_prefab = (GameObject)Resources.Load("GameManager", typeof(GameObject));
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Explotion"))
        {

            //Create small particle system of explotion

            BlocksOnScreen.Obj.BlockDestroyed();
            Debug.Log("blocsssss");

            if (Random.Range(0.0f, 1.0f) > 0.3f)
                Instantiate(powerup_prefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}