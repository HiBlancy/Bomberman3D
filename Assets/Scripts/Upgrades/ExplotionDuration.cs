using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionDuration : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DisableElement", 4f);
    }

    void DisableElement()
    {
        gameObject.SetActive(false);
    }
}
