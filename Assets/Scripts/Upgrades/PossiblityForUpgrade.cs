using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossiblityForUpgrade : MonoBehaviour
{
    public List<GameObject> upgradePrefabs; // Lista de prefabs para diferentes tipos de upgrades.

    public static PossiblityForUpgrade Obj { get; private set; }

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Explotion"))
        {
            GenerateRandomUpgrade();
            BlocksOnScreen.Obj.BlockDestroyed();
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
    //public void BloqueDestruido()
    //{        
    //    GenerateRandomUpgrade();
    //    //gameObject.SetActive(false);
    //    BlocksOnScreen.Obj.BlockDestroyed();
    //    Destroy(this.gameObject);        
    //}

    private void GenerateRandomUpgrade()
    {
        int randomIndex = Random.Range(0, upgradePrefabs.Count);
        GameObject upgradePrefab = upgradePrefabs[randomIndex];

        _ = Instantiate(upgradePrefab, transform.localPosition, Quaternion.identity);

        //IUpgrade upgrade = Instantiate(upgradePrefab, transform.position, Quaternion.identity)?.GetComponent<IUpgrade>();
    }
    //private void OnDisable()
    //{
    //    Destroy(gameObject);
    //}
}