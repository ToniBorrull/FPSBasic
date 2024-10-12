using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjects : MonoBehaviour
{
    public LayerMask layerMask;
    public float speed = 10f;
    public float distance;
    public bool objectFront;
    RaycastHit hit;
    public GameObject selectedObject;
    private Rigidbody selectedRb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
        PickupObject();
    }

    void Raycast()
    {
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            objectFront = true;
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
            objectFront = false;
            Debug.Log("Not Hit");
        }
    }
    void PickupObject()
    {
        if (objectFront)
        {
            if (Input.GetButton("Fire1"))
            {
                selectedObject = hit.transform.gameObject;
                selectedRb = selectedObject.GetComponent<Rigidbody>();
                
                selectedRb.useGravity = false;
                selectedObject.transform.position = transform.position + transform.forward * distance;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                
                selectedRb.useGravity = true;
                selectedObject = null;
                selectedRb = null;  // Limpiar la referencia al Rigidbody
            }
        }
    }
}
