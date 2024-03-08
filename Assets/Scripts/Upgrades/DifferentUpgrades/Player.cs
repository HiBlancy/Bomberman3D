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

    public float moveSpeed = 5f; //
    public int bombs = 1;  //
    public int explosion_power = 2; //
    public int lifes = 1; //
    public float speedbomb = 10f; //

    bool isImmune = false;
    float immuneTimeCnt = 0f;
    float immuneTime = 2f;

    void Update()
    {
        if (isImmune)
        {
            immuneTimeCnt -= Time.deltaTime;

            if (immuneTimeCnt <= 0)
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Explotion"))
        {
            LoseHealth();
            Debug.Log("It Will Take 1 life");
        }
    }

    public void LoseHealth()
    {
        lifes--;
        UpdatePowerUpsOnScreen(POWERUPS.PlayerHealth);
        // takeDamageSound.Play();

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
        immuneTimeCnt = immuneTime;
    }

    public void CheckOnLifes()
    {
        if (lifes <= 3)
        {
            lifes = 3;
        }
    }
}