using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Obj { get; private set; }

    [SerializeField] GameObject gameOverPanel;
    private Player player;

    //[SerializeField] AudioSource gameOverSound;
    //[SerializeField] AudioSource takeDamageSound;

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }

    void Start()
    {
        player = GetComponent<Player>();
    }

    public void LoseHealt()
    {
        player.lifes--;
        // takeDamageSound.Play();

        if (player.lifes <= 0)
        {
           // gameOverSound.Play();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        //TextOnScreen.Obj.UpdateOnScreen();
    }

    public void GiveHeath()
    {
        if(player.lifes! <= 3)
        {
            player.lifes++;
        }
    }
}