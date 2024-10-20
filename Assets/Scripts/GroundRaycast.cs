using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycast : MonoBehaviour
{
    public LayerMask layerMask;
    
    public float distance;
    public bool grounded;
    public float jumpForce;

    public float jetpackForce;
    bool jumping = false;

    public float speed = 10f;
    float normalSpeed;
    Vector3 dir;


    public bool pressingSpace = false;
    public int pressedTimes = 0;
    public int lastTime;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        normalSpeed = speed;
    }



    void Update()
    {
       Space();
       Raycast();
       Jump();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Space()
    {
        
        if (Input.GetButton("Jump"))
        {
            SpaceDown();
        }
        else
        {
            SpaceUp();
        }
    }
    void Jump()
    {
        if (pressingSpace)
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
            ResetSpace();
        }
        if (!grounded)
        {
            speed = normalSpeed * 0.5f;
        }
        float verticalMovement = vertical * Time.fixedDeltaTime * speed;
        float horizontalMovement = horizontal * Time.fixedDeltaTime * speed;
        dir = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);
        transform.localPosition += dir;

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

    void SpaceDown()
    {
            pressingSpace = true;
            if(pressedTimes == lastTime)
            {
                lastTime += 1;
            }
    }
    void SpaceUp()
    {
        pressingSpace = false;
        
    }

    public void ResetSpace()
    {
        pressedTimes = 0;
    }

}
