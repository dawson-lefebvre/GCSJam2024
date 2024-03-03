using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Haha the fog Persona 4 Golden
public class TheFog : MonoBehaviour
{
    bool fogActive = true;

    private void Update()
    {
        if(!fogActive)
        {
            SpriteRenderer ren = GetComponent<SpriteRenderer>();
            ren.color -= new Color(0,0,0,1 * Time.deltaTime);
            if(ren.color.a <= 0)
            {
                Destroy(transform.root.gameObject);
            }
        }
    }

    public void EnableFog()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableFog()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        fogActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.root.AddComponent<PlayerHurt>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(collision.GetComponentInParent<PlayerHurt>());
        }
    }
}
