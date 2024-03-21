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
    public GameObject kickbomb;

    private GameObject curr;

    public POWERUPS powerup;

    public void Start()
    {
        powerup = (POWERUPS)Random.Range(0, 5);
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
            case POWERUPS.KickBomb:
                curr = kickbomb;
                break;
            default:
                break;
        }
        GameObject go = Instantiate(curr, transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<Transform>().SetParent(this.transform);
        Invoke("SelfDestroy", 10f);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
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
                    player.moveSpeed = player.moveSpeed + 0.5f;
                    break;
                case POWERUPS.BombPower:
                    Debug.Log("BombPower");
                    player.explosion_power++;
                    break;
                case POWERUPS.BombSpeed:
                    Debug.Log("BombSpeed");
                    player.speedbomb--;
                    break;
                case POWERUPS.BombCount:
                    Debug.Log("BombCount");
                    player.bombs++;
                    player.CheckOnBombs();
                    break;
                case POWERUPS.PlayerHealth:
                    Debug.Log("PlayerHealth");
                    player.lifes++;
                    player.CheckOnLifes();
                    break;
                case POWERUPS.KickBomb:
                    Debug.Log("Can KickBomb");
                    player.kickBomb = true;
                    break;
            }

            if (collider.CompareTag("AI"))
            {
                AIController Ai = collider.GetComponent<AIController>();

                switch (powerup)
                {
                    case POWERUPS.PlayerSpeed:
                        Debug.Log("AISpeed");
                        Ai.AIspeed = Ai.AIspeed + 0.5f;
                        break;
                    case POWERUPS.BombPower:
                        Debug.Log("AIBombPower");
                        Ai.explosion_power++;
                        break;
                    case POWERUPS.BombSpeed:
                        Debug.Log("AIBombSpeed");
                        Ai.speedbomb--;
                        break;
                    case POWERUPS.BombCount:
                        Debug.Log("AIBombCount");
                        Ai.bombs++;
                        Ai.CheckOnBombs();
                        break;
                    case POWERUPS.PlayerHealth:
                        Debug.Log("AIHealth");
                        Ai.lifes++;
                        Ai.CheckOnLifes();
                        break;
                    case POWERUPS.KickBomb:
                        Debug.Log("AI Can KickBomb");
                        Ai.kickBomb = true;
                        break;
                }

                Destroy(gameObject);
                player.UpdatePowerUpsOnScreen(powerup);
            }
        }
    }
}