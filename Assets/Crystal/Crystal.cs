using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    public bool isLastOne = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerManager.Grow();
            if (isLastOne)
            {
                playerManager.Fade();
            }
            Destroy(gameObject);
        }
    }
}
