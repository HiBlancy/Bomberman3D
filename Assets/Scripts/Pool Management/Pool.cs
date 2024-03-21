using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] GameObject poolElementPrefab;
    List<GameObject> _pool = new List<GameObject>();

    public int amoutToPool;

    void Awake()
    {
        for (int i = 0; i < amoutToPool; i++)
        {
            GameObject prefabFromPool = Instantiate(poolElementPrefab);
            prefabFromPool.SetActive(false);
            _pool.Add(prefabFromPool);
        }
    }

    public GameObject GetElement()
    {
        for (int j = 0; j < _pool.Count; j++)
        {
            if (!_pool[j].activeInHierarchy)
            {
                return _pool[j];
            }
        }
        return null;
    }

    public void ReturnElement(GameObject elementToReturn)
    {
        elementToReturn.SetActive(false);
    }
}