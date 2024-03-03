using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Character")]
    [SerializeField] Animator animator = null;
    [SerializeField] Transform puppet = null;

    [Header("Tail")]
    [SerializeField] Transform tailAnchor = null;
    [SerializeField] Rigidbody2D tailRigidbody = null;

    [Header("Equipment")]
    [SerializeField] Transform handAnchor = null;
    [SerializeField] UnityEngine.U2D.Animation.SpriteLibrary spriteLibrary = null;

    //Components
    public Rigidbody2D rb;
    PlayerManager playerManager;
    SpriteRenderer spriteRenderer;

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
        playerManager = GetComponent<PlayerManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Read values
        moveValue = moveAction.ReadValue<Vector2>();

        if(moveValue.x < 0)
        {
            if(!spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (moveValue.x > 0)
        {
            if (spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }

        //Jump
        if (!playerManager.canClimb)
        {
            if (jumpAction.WasPressedThisFrame())
            {
                if (IsGrounded())
                {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //Add forces
        rb.velocity = new Vector2(moveValue.x * maxSpeed, rb.velocity.y);

        //Climbing
        if (playerManager.canClimb)
        {
            if (rb.gravityScale > 0)
            {
                rb.gravityScale = 0;
            }

            transform.Translate(Vector2.up * moveValue.y * maxSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (rb.gravityScale == 0)
            {
                rb.gravityScale = 4;
            }
        }
    }

    bool IsGrounded()
    {
        Bounds bounds = GetComponentInChildren<CapsuleCollider2D>().bounds;
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - bounds.extents.y - 0.1f), Vector2.down);
        if(hit.distance <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
