using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stump : MonoBehaviour
{
    public float dropSpeed = 2;
    bool isDropping = false;
    float amountToDrop = 0;
    float amountDropped = 0;
    Bounds stumpBounds;
    bool spawnGem = false;
    [SerializeField] GameObject gem;
    private void Start()
    {
        stumpBounds = GetComponentInChildren<BoxCollider2D>().bounds;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Start dropping
            int playerSize = collision.GetComponentInParent<PlayerManager>().currentSize;
            switch (playerSize)
            {
                case 1:
                    amountToDrop = stumpBounds.size.y / 6;
                    spawnGem = false;
                    break;
                case 2:
                    amountToDrop = stumpBounds.size.y / 4;
                    spawnGem = false;
                    break;
                case 3:
                    amountToDrop = stumpBounds.size.y / 2;
                    spawnGem = true;
                    break;
                default:
                    break;
            }
            isDropping = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isDropping = false;
            gem.SetActive(false);
        }
    }

    private void Update()
    {
        //Move down or up
        if (isDropping)
        {
            if (amountDropped < amountToDrop)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * dropSpeed, 0);
                amountDropped += Time.deltaTime;
            }
            else if (!gem.activeInHierarchy)
            {

                gem.SetActive(true);

            }
        }
        else
        {
            if (amountDropped > 0)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * dropSpeed);
                amountDropped -= Time.deltaTime;
            }
        }
    }
}
