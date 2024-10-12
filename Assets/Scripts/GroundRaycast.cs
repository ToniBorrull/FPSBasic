using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRaycast : MonoBehaviour
{
    public LayerMask layerMask;
    public float speed = 10f;
    public float distance;
    public bool grounded;
    public float jumpForce;

    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            float vertical = Input.GetAxis("Vertical") * Time.deltaTime * speed;
            float horizontal = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
            Vector3 dir = (transform.forward * vertical) + (transform.right * horizontal);
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
