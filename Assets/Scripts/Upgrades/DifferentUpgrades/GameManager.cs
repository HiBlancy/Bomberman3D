using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerspeed;
    public GameObject bombpower;
    public GameObject bombspeed;
    public GameObject bombcount;
    public GameObject playerhealth;

    private GameObject curr;

    public POWERUPS powerup;

    public void Start()
    {
        powerup = (POWERUPS)Random.Range(0, 4);
        switch (powerup)
        {
            case POWERUPS.PlayerSpeed:
                curr = playerspeed;
                break;
            case POWERUPS.BombPower:
                curr = bombpower;
                break;
            case POWERUPS.BombSpeed:
                curr = bombspeed;
                break;
            case POWERUPS.BombCount:
                curr = bombcount;
                break;
            case POWERUPS.PlayerHealth:
                curr = playerhealth;
                break;
            default:
                break;
        }
        GameObject go = Instantiate(curr, transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<Transform>().SetParent(this.transform);
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {

            Player player = collider.GetComponent<Player>();

            switch (powerup)
            {
                case POWERUPS.PlayerSpeed:
                    Debug.Log("PlayerSpeed");
                    player.moveSpeed++;
                    break;
                case POWERUPS.BombPower:
                    Debug.Log("BombPower");
                    player.explosion_power++; //mirar si +1 o +2
                    break;
                case POWERUPS.BombSpeed:
                    Debug.Log("BombSpeed");
                    player.speedbomb--;
                    break;
                case POWERUPS.BombCount:
                    Debug.Log("BombCount");
                    player.bombs++;
                    break;
                case POWERUPS.PlayerHealth:
                    Debug.Log("PlayerHealth");
                    player.lifes++;
                    break;
            }
            Destroy(gameObject);
            player.UpdatePowerUpsOnScreen(powerup);
        }
    }
}
