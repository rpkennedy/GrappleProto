using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private float movementX;
    private float movementY;
    public bool isGrappled;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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