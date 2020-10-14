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

    private float movementX;
    private float movementY;
    private bool isJumping;
    public bool isGrappled;

    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        isGrappled = false;
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        if (isGrappled) movementY = Input.GetAxis("Vertical");

        
        if (Input.GetKeyDown("space") && coll.IsTouching(ground)) isJumping = true;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, movementY, 0.0f);
        rb.AddForce(movement * speed);

        
    }

}