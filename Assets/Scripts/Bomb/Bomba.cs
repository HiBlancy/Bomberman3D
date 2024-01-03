using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float tiempoExplosion = 3f;
    public float radioExplosion = 2f;
    //public LayerMask capasObjetosDestructibles;
    public GameObject sistemaParticulasExplosion;

    private bool exploded = false;

    public void SummonBomb(Vector3 startPosition)
    {
        gameObject.SetActive(true);
        this.transform.position = startPosition;
        Invoke("Explotar", tiempoExplosion);
    }

    void Explotar()
    {
        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        BombCount.Obj.bombsOnScreen--;

        CreateExplosions(Vector3.forward);
        CreateExplosions(Vector3.right);
        CreateExplosions(Vector3.back);
        CreateExplosions(Vector3.left);

        exploded = true;
    }

    void CreateExplosions(Vector3 posicion)
    {
        List<Vector3> instantiate_list = new List<Vector3>();

        for (float i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Vector3 direccion = transform.position + posicion * i;

            //Physics.Raycast(transform.position, direccion, out hit, i);

            Debug.DrawLine(transform.position, direccion, Color.green, 5f);


            if (Physics.Raycast(transform.position, direccion, out hit, i))
            {
                if (hit.collider.CompareTag("Block"))
                {
                    instantiate_list.Add(transform.position + (i * posicion));
                    Debug.Log("block");
                }
                else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("powerup") || hit.collider.CompareTag("Bomb"))
                {
                    instantiate_list.Add(transform.position + (i * posicion));
                    Debug.Log("other");
                }
            }
            else
            {      
                instantiate_list.Add(transform.position + (i * posicion));
                Debug.Log("noting");
            }
        }
        foreach (Vector3 explosiveZone in instantiate_list)
        {
            Instantiate(sistemaParticulasExplosion, explosiveZone, sistemaParticulasExplosion.transform.rotation);
        }
    }

        void OnCollisionEnter(Collision collision)
        {
            if (!exploded && collision.collider.CompareTag("Explotion"))
            {
                CancelInvoke("Explotar");
                Explotar();
            }
        }
}  