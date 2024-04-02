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
                    player.moveSpeed = player.moveSpeed + 0.5f;
                    break;
                case POWERUPS.BombPower:
                    player.explosion_power++;
                    break;
                case POWERUPS.BombSpeed:
                    player.speedbomb--;
                    break;
                case POWERUPS.BombCount:
                    player.bombs++;
                    player.CheckOnBombs();
                    break;
                case POWERUPS.PlayerHealth:
                    player.lifes++;
                    player.CheckOnLifes();
                    break;
                case POWERUPS.KickBomb:
                    player.kickBomb = true;
                    break;
            }
                Destroy(gameObject);
                player.UpdatePowerUpsOnScreen(powerup);
        }

        if (collider.CompareTag("AI"))
        {
            AIController Ai = collider.GetComponent<AIController>();

            switch (powerup)
            {
                case POWERUPS.PlayerSpeed:
                    Ai.AIspeed = Ai.AIspeed + 0.5f;
                    break;
                case POWERUPS.BombPower:
                    Ai.explosion_power++;
                    break;
                case POWERUPS.BombSpeed:
                    Ai.speedbomb--;
                    break;
                case POWERUPS.BombCount:
                    Ai.bombs++;
                    Ai.CheckOnBombs();
                    break;
                case POWERUPS.PlayerHealth:
                    Ai.lifes++;
                    Ai.CheckOnLifes();
                    break;
                case POWERUPS.KickBomb:
                    Ai.kickBomb = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}