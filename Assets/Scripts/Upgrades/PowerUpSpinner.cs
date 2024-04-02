using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpinner : MonoBehaviour
{
    [SerializeField] float valorX;
    [SerializeField] float valorY;
    [SerializeField] float valorZ;
    void FixedUpdate()
    {
        transform.Rotate(valorX * Time.deltaTime, valorY * Time.deltaTime, valorZ * Time.deltaTime);
    }
}
