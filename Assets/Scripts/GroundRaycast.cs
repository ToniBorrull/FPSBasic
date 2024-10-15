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

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalSpeed = speed;
    }



    void Update()
    {
       MovePlayer();
       Jump();
       Raycast();
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
                if (Input.GetButtonUp("Fire3")){
                    speed = normalSpeed;
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

            float verticalMovement = vertical * Time.deltaTime * speed;
            float horizontalMovement = horizontal * Time.deltaTime * speed;

            Vector3 dir = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);
            transform.localPosition += dir;
            
        }
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
