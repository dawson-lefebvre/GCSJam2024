using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody2D rb;

    //Input
    [Header("Input Actions")]
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;

    Vector2 moveValue;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    //Movement vars
    [Header("Movement Values")]
    public float acceleration = 1;
    public float maxSpeed = 5;
    public float jumpForce = 3;
    // Start is called before the first frame update
    void Start()
    {
        //Get components
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Read values
        moveValue = moveAction.ReadValue<Vector2>();

        //Jump
        if (jumpAction.WasPressedThisFrame())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Add forces
        rb.velocity = new Vector2(moveValue.x * maxSpeed, rb.velocity.y);

        ////Clamp speed
        //if (rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed)
        //{
        //    rb.velocity = new Vector2(rb.velocity.normalized.x * maxSpeed, rb.velocity.y);
        //}
    }
}
