using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] Text bombcount_label;
    [SerializeField] Text life_label;
    [SerializeField] Text bombspeed_label;
    [SerializeField] Text explosion_label;
    [SerializeField] Text speedplayer_label;

    public float moveSpeed = 5f;
    public int bombs = 1;
    public int explosion_power = 2;
    public int lifes = 1;
    public float speedbomb = 10f;   

    public void UpdatePowerUpsOnScreen(POWERUPS powerup)
    {
        switch(powerup)
        {
            case POWERUPS.BombCount:
                bombcount_label.text = bombs.ToString();
                break;
            case POWERUPS.BombPower:
                explosion_label.text = explosion_power.ToString();
                break;
            case POWERUPS.BombSpeed:
                bombspeed_label.text = speedbomb.ToString();
                break;
            case POWERUPS.PlayerSpeed:
                speedplayer_label.text = moveSpeed.ToString();
                break;
            case POWERUPS.PlayerHealth:
                life_label.text = lifes.ToString();
                break;
        }
    }
}