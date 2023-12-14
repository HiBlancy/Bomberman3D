using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Obj { get; private set; }

    [SerializeField] GameObject gameOverPanel;

    //[SerializeField] AudioSource gameOverSound;
    //[SerializeField] AudioSource takeDamageSound;
    public int playerLives;
    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    void Start()
    {
        playerLives = 3;
    }

    public void LoseHealt()
    {
        playerLives--;
        // takeDamageSound.Play();

        if (playerLives <= 0)
        {
           // gameOverSound.Play();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        //TextOnScreen.Obj.UpdateOnScreen();
    }

    public void GiveHeath()
    {
        if(playerLives !<= 3)
        {
            playerLives++;
        }
    }
}