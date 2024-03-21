using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] Text bombcount_label;
    [SerializeField] Text life_label;
    [SerializeField] Text bombspeed_label;
    [SerializeField] Text explosion_label;
    [SerializeField] Text speedplayer_label;   
    [SerializeField] Text kickbomb_label;   

    public float moveSpeed = 5f; //
    public int bombs = 1;  //
    public int explosion_power = 2; //
    public int lifes = 2; //
    public float speedbomb = 10f; //
    public bool kickBomb = false;

    int maxBombs = 10;

    bool isImmune = false;
    float immuneTime = 2f;

    PlayerCollisionBomb playerhurtsound;

    void Awake()
    {
        playerhurtsound = GameObject.Find("Capsule Mesh").GetComponent<PlayerCollisionBomb>();
    }

    void Update()
    {
        if (isImmune)
        {
            immuneTime -= Time.deltaTime;

            if (immuneTime <= 0)
                isImmune = false;
        }
    }

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
            case POWERUPS.KickBomb:
                kickbomb_label.text = kickBomb.ToString();
                break;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Explotion") && !isImmune)
        {
            LoseHealth();
            playerhurtsound.HurtSound();
        }
    }

    void LoseHealth()
    {
        lifes--;
        UpdatePowerUpsOnScreen(POWERUPS.PlayerHealth);

        goImmune();

        if (lifes == 0)
        {
            // gameOverSound.Play();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    void goImmune()
    {
        isImmune = true;
    }

    public void CheckOnLifes()
    {
        if (lifes >= 3)
            lifes = 3;
    }

    public void CheckOnBombs()
    {
        if (maxBombs <= bombs)
            bombs = 10;
    }
}