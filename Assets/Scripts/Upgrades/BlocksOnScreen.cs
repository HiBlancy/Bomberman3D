using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlocksOnScreen : MonoBehaviour
{
    public static BlocksOnScreen Obj { get; private set; }

    public int blocksLeft;

    void Awake()
    {
        if (Obj != null && Obj != this)
            Destroy(this);
        else
            Obj = this;
    }
    void Start()
    {
        blocksLeft = GameObject.FindGameObjectsWithTag("Block").Length;
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        if (blocksLeft <= 0)
        {
            Debug.Log("No more blocks on screen, make text on screen that you should kill the other player");
        }
            //Finish Game
    }
}