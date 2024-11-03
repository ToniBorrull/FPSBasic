using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycast : MonoBehaviour
{
    public LayerMask layerMask;
    
    public float distance;
    public bool grounded;
    public float jumpForce;
    bool jumping = false;

    public float speed;
    float normalSpeed;
    Vector3 dir;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalSpeed = speed;
    }


    void Update()
    {
       Raycast();
       Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                if (!jumping)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    jumping = true;
                }
            }
        }
        else
        {
            jumping = false;
        }
    }
    void MovePlayer()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (grounded)
        {
            speed = normalSpeed;

            if (vertical > 0)
            {
                speed = normalSpeed;

                if (Input.GetButton("Fire3")){
                    speed = normalSpeed * 2;

                }
                
            }

            if (vertical < 0)
            {
                speed = normalSpeed * 0.5f;
            }
            if (horizontal != 0 && vertical == 0)
            {
                speed = normalSpeed * 0.75f; 
            } 
        }
        if (!grounded)
        {
            speed = normalSpeed * 0.5f;
        }
        dir = (transform.forward * vertical) + (transform.right * horizontal);

        if (dir.magnitude >= 1)
        {
            dir.Normalize();
        }
        transform.localPosition += dir * Time.fixedDeltaTime * speed;
    }
    void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            grounded = true;
            
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
            grounded = false;
            
        }
    }

 

}
