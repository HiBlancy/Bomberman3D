using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float tiempoExplosion = 3f;
    public float radioExplosion = 2f;
    public LayerMask capasObjetosDestructibles;
    public GameObject sistemaParticulasExplosion;

    public void SummonBomb(Vector3 startPosition)
    {
        gameObject.SetActive(true);
        this.transform.position = startPosition;
        Invoke("Explotar", tiempoExplosion);
    }

    void Explotar()
    {
        // Desactivar el objeto visual de la bomba
        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        BombCount.Obj.bombsOnScreen--;

        // Activar el sistema de partículas de la explosión
        GameObject particulasExplosion = Instantiate(sistemaParticulasExplosion, transform.position, Quaternion.identity);

        // Lógica de detección de colisiones y daño (como se describió en respuestas anteriores)
        ExplorarEnDireccion(Vector3.forward);
        ExplorarEnDireccion(Vector3.back);
        ExplorarEnDireccion(Vector3.left);
        ExplorarEnDireccion(Vector3.right);
    }

    void ExplorarEnDireccion(Vector3 direccion)
    {
        for (float i = 1; i <= radioExplosion; i++)
        {
            Vector3 posicion = transform.position + direccion * i;

            Debug.DrawLine(posicion, direccion, Color.red, 5f);

            // Lanzar rayo en la dirección especificada
            RaycastHit hit;
            if (Physics.Raycast(posicion, direccion, out hit, i, capasObjetosDestructibles))
            {
                if (hit.collider.CompareTag("Block"))
                {
                    // Destruir bloque y generar upgrade
                   // Destroy(hit.collider.gameObject);
                    PossiblityForUpgrade.Obj.BloqueDestruido();
                }
            }
        }
    }

}
