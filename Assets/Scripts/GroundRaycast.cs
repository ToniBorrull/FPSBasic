using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycast : MonoBehaviour
{
    public LayerMask layerMask;
    
    public float distance;
    public bool grounded;
    public float jumpForce;

    public float speed = 10f;
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
      
       Jump();
       Raycast();
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
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    void MovePlayer()
    {
        if (grounded)
        {
            float vertical = Input.GetAxis("Vertical");
            

            float horizontal = Input.GetAxis("Horizontal");

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

            float verticalMovement = vertical * Time.fixedDeltaTime * speed;
            float horizontalMovement = horizontal * Time.fixedDeltaTime * speed;

             dir = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);
            
            
        }
        if (!grounded)
        {
            speed = normalSpeed * 0.5f;
        }
        transform.localPosition += dir;

    }
    void Raycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, Vector3.down * hit.distance, Color.yellow);
            grounded = true;
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, Vector3.down * distance, Color.red);
            grounded = false;
            Debug.Log("Not Hit");
        }
    }
}
