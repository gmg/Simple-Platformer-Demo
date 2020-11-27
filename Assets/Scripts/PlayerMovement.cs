using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpPower = 12f;
    [SerializeField] float groundCheckDistance = 0.0625f;
    [SerializeField] LayerMask whatIsGround;
    
    float moveInput;
    Rigidbody2D rb = null;
    bool onGround = true;
    Animator anim;

    [Header("Game Feel")]
    [SerializeField] float hangTimeDurarion = 0.2f;
    [SerializeField] float preJumpDuration = 0.2f;    

    float hangTime = 0;
    float preJump = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // get horizontal input
        moveInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            preJump = preJumpDuration;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        // check if on ground
        onGround = GroundCheck();
        
        // set hangTime
        if(onGround)
        {
            hangTime = hangTimeDurarion;
        }
        else
        {
            hangTime -= Time.deltaTime;
        }

        // update prejump
        if (preJump > 0) preJump -= Time.deltaTime;

        // apply horizontal movement
        rb.velocity = new Vector2(runSpeed * moveInput, rb.velocity.y);
        
        // jump if needed
        if (preJump > 0 && hangTime > 0)
        {
            //rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            Debug.Log("release jump");
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        // update animations
        anim.SetFloat("runSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("onGround", onGround);
        
        // flip sprite
        if (rb.velocity.x > 0 && transform.localScale.x < 0)
        {
            Flip();
        }
        else if (rb.velocity.x < 0 && transform.localScale.x > 0)
        {
            Flip();
        }

        // tuned gravity
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = 3;
        }
        else if (rb.velocity.y < 0)
        {
            rb.gravityScale = 6;
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(0.45f, 0.1f), 0, whatIsGround);
        //return Physics2D.Linecast(
        //    transform.position,
        //    transform.position + (Vector3.down * groundCheckDistance),
        //    whatIsGround);
    }

    public void Die()
    {
        GameEvents.OnPlayerDied?.Invoke();
        gameObject.SetActive(false);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector2(0.45f, 0.1f));
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 200, 50), $"On Ground: {onGround}");
    }
}
