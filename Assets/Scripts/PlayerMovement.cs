using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpPower = 12f;
    [SerializeField] float groundCheckDistance = 0.0625f;
    [SerializeField] LayerMask whatIsGround;
    
    float moveInput;
    //bool jumpPressed;
    Rigidbody2D rb = null;
    bool onGround = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        moveInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // check if on ground
        onGround = GroundCheck();
        
        // apply horizontal movement
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
        
        // jump if needed
        if (Input.GetButton("Jump") && onGround)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.Linecast(
            transform.position,
            transform.position + (Vector3.down * groundCheckDistance),
            whatIsGround);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 50), $"On Ground: {onGround}");
    }
}
