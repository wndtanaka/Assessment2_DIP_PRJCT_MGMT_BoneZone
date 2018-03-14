using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7;
    public float rotateSpeed = 90;
    public bool isGrounded = false;
    public bool isJumping = false;
    public Vector3 moveDirection = Vector3.zero;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float inputH = Input.GetAxis("Horizontal");
        //float inputV = Input.GetAxis("Vertical");

        // set velocity to zero
        rb.velocity = Vector3.zero;

        // checks if there is anything under character if is set grounded to true if not set it to false
        if (Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        // checks if we grounded and pressed space key, then we can jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            // TODO Jump
        }
        else
        {
            //isJumping = false;
        }

        // press A to rotate to the left and D to the right
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -Mathf.Clamp(180f * Time.deltaTime, 0f, 360f));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, Mathf.Clamp(180f * Time.deltaTime, 0f, 360f));
        }

        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        moveDirection = new Vector3(inputH, 0, inputV);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        //we set rigidbodys velocity to our movedirection wich contains speed and rotation of our character
        rb.velocity = moveDirection;
    }
}

