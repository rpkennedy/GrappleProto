using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private CircleCollider2D coll;
    public BoxCollider2D ground;
    public HookController hook;
    public RopeController rope;

    private float movementX;

    public bool isClimbing;
    public bool isDescending;
    private bool isJumping;
    public bool isGrappled;
    GameObject node;

    void Start()
    {        
        hook = GetComponent<HookController>();
        rope = hook.GetComponent<RopeController>();
        coll = GetComponent<CircleCollider2D>();
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        isGrappled = false;
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        if (hook.grapple != null)                       //if we have a rope
        {
            if (Input.GetKey("w"))
            {
                isClimbing = true;
                isDescending = false;
            }
            else isClimbing = false;
            if (Input.GetKey("s"))
            {
                isDescending = true;
                isClimbing = false;
            }
            else isDescending = false;
        } 

        
        if (Input.GetKeyDown("space") && coll.IsTouching(ground)) isJumping = true;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0f, 0f);      //L - R move
        rb.AddForce(movement * speed);                          

        if (isJumping)                                          //jump
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isJumping = false;
        }        
                    //climb / descend is in RopeController bc reasons --apologies future self--
    }
}