using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{ 

    public void EnableFog()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableFog()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        //Deal Damage to Player
    }
}
