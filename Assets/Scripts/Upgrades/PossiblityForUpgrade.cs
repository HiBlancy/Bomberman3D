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
            NavMeshManager.Instance.BakeNavMesh();
            //Create small particle system of explotion?

            if (Random.Range(0.0f, 1.0f) > 0.5f)
                Instantiate(powerup_prefab, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        NavMeshManager.Instance.BakeNavMesh();
        if (Random.Range(0.0f, 1.0f) > 0.5f)
            Instantiate(powerup_prefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}