using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jetpack : MonoBehaviour
{
    GroundRaycast raycast;
    Rigidbody rb;
    public float jetpackForce;
    bool jetpackOn = false;
    public float jetpackFuel;
    public float maxFuel;
    public Image fuelCanvas;
    public Image fuelCanvas2;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        jetpackFuel = maxFuel;
        raycast = GetComponent<GroundRaycast>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _Jetpack();
    }
    void _Jetpack()
    {
        ActivateJetpack();
        UsingJetpack();
        JetpackRefuel();
        FuelCanvas();
    }
    void ActivateJetpack()
    {
        if (!raycast.grounded)
        {
            
            if(raycast.pressingSpace)
            {
                if (raycast.pressedTimes == 2)
                {
                    JetpackOn();
                }

            }
            if (!raycast.pressingSpace)
            {
                JetpackOff();
            }
        }
    }
    void JetpackOn()
    {   
        jetpackOn = true;
    }
    void JetpackOff()
    {
        jetpackOn = false;
    }
    void UsingJetpack()
    {
        if (jetpackOn)
        {
            if (jetpackFuel > 0)
            {
                rb.AddForce(rb.transform.up * jetpackForce, ForceMode.Impulse);
                jetpackFuel -= Time.deltaTime;  
            }
        }
    }
    void JetpackRefuel()
    {
        if (raycast.grounded)
        {
            if (jetpackFuel > 0)
            {
                if (jetpackFuel < maxFuel)
                {
                    jetpackFuel += Time.deltaTime;
                }
            }
            if(jetpackFuel <= 0)
            {
                timer += Time.deltaTime;
                if(timer > 0.5f)
                {
                    if (jetpackFuel < maxFuel)
                    {
                        jetpackFuel += Time.deltaTime;
                        timer = 0;
                    }
                }
            }
        }
    }
    void FuelCanvas()
    {
        fuelCanvas.fillAmount = jetpackFuel / maxFuel;
        fuelCanvas2.fillAmount = jetpackFuel / maxFuel;
    }

    
}
