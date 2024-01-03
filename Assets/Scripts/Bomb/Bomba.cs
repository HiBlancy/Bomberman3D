using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float tiempoExplosion = 3f;
    public int radioExplosion = 2;
    public LayerMask capasObjetosDestructibles;
    public GameObject sistemaParticulasExplosion;

    bool exploded = false;

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

    void CreateExplosions(Vector3 direccion)
    {
        List<Vector3> instantiate_list = new List<Vector3>();

        for (float i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Vector3 raycastPosition = transform.position + direccion * i;

            Physics.Raycast(transform.position, direccion, out hit, i, capasObjetosDestructibles);
            Debug.DrawLine(transform.position, raycastPosition, Color.green, 5f);

            if (!hit.collider)
            {
                instantiate_list.Add(raycastPosition);
                Debug.Log("nothing");
            }
            else
            {
                if (hit.collider.CompareTag("Block"))
                {
                    instantiate_list.Add(raycastPosition);
                    Debug.Log("block");
                }
                else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("powerup") || hit.collider.CompareTag("Bomb"))
                {
                    instantiate_list.Add(raycastPosition);
                    Debug.Log("other");
                    continue;
                }
                break;
            }
        }
        foreach (Vector3 explosionPos in instantiate_list)
            Instantiate(sistemaParticulasExplosion, explosionPos, Quaternion.identity);
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