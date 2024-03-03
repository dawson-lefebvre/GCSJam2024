using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{   
    PlayerManager playerManager;
    SpriteRenderer playerSpriteRenderer;
    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public float maxFrames = 30, frames = 0;
    // Update is called once per frame
    void Update()
    {
        frames++;
        if(frames >= maxFrames)
        {
            frames = 0;
            if(playerSpriteRenderer.color == Color.white)
            {
                playerSpriteRenderer.color = Color.red;
                playerManager.HurtPlayer(1);
            }
            else
            {
                playerSpriteRenderer.color = Color.white;
            }
        }
    }

    private void OnDestroy()
    {
        playerManager.health = 20;
        playerSpriteRenderer.color = Color.white;
    }
}
