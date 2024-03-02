using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomControl : MonoBehaviour
{
    [SerializeField] int jumpForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.root.GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpForce);
    }

}
