using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public int explosion_power = 2;
    public LayerMask capasObjetosDestructibles;

    [SerializeField] AudioSource audioClip;

    Player player;

    private void Awake()
    {
        audioClip = GetComponent<AudioSource>();

        player = GameObject.Find("First Person Controller").GetComponent<Player>();
    }

    public void SummonBomb(Vector3 startPosition)
    {
        gameObject.SetActive(true);
        this.transform.position = startPosition;
        Invoke("Explotar", player.speedbomb);
    }

    void Explotar()
    {
        audioClip.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;

        Invoke("DestroySelf", 0.6f);

        CreateExplosions(Vector3.forward);
        CreateExplosions(Vector3.right);
        CreateExplosions(Vector3.back);
        CreateExplosions(Vector3.left);
    }

    void CreateExplosions(Vector3 direccion)
    {
        GameObject explotionPool = PoolManager.Obj.ExplotionPool.GetElement();

        List<Vector3> instantiate_list = new List<Vector3>();

        for (float i = 1; i < explosion_power; i++)
        {
            RaycastHit hit;
            Vector3 raycastPosition = transform.position + direccion * i;

            Physics.Raycast(transform.position, direccion, out hit, i);

            if (!hit.collider)
            {
                instantiate_list.Add(raycastPosition);
            }
            else
            {
                if (hit.collider.CompareTag("Block"))
                {
                    instantiate_list.Add(raycastPosition);
                    hit.collider.gameObject.GetComponent<PossiblityForUpgrade>().enabled = true;

                }
                else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("powerup") || hit.collider.CompareTag("AI"))
                {
                    instantiate_list.Add(raycastPosition);
                    continue;
                }
                break;
            }
        }
        foreach (Vector3 explosionPos in instantiate_list)
        {
            ExplotionDuration explotionBehaviour = explotionPool.GetComponent<ExplotionDuration>();
            explotionBehaviour.ActivateExplotion(explosionPos);
        }           
    }

    void DestroySelf()
    {
        PlayerInstBomb.Obj.BombExploded();

        PoolManager.Obj.BombPool.ReturnElement(this.gameObject);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}