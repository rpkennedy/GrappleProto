using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;

    private float movementX;
    private float movementY;
    public bool isGrappled;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrappled = false;
    }

    void Update()
    {
        movementX = Input.GetAxis("Horizontal");
        if (isGrappled) movementY = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

}