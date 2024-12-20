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
    float jetpackFuel;
    public float maxFuel;
    public float fuelConsumption;
    float rechargeJetpack;
    public Image fuelCanvas;
    public Image fuelCanvas2;

    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        jetpackFuel = maxFuel;
        raycast = GetComponent<GroundRaycast>();
        rb = GetComponent<Rigidbody>();

        //Hago que siempre tarde 0.5 en recargar sin importar cuanto fuel tenga de max
        rechargeJetpack = maxFuel / 0.5f;
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
            if (Input.GetButtonDown("Jump"))
            {
                jetpackOn = true;
            }

            if(Input.GetButtonUp("Jump"))
            {
                jetpackOn = false;
            }
        }
    }
    void UsingJetpack()
    {
        if (jetpackOn)
        {
            if (jetpackFuel > 0)
            {
                rb.AddForce(rb.transform.up * jetpackForce * Time.deltaTime, ForceMode.Force);
                jetpackFuel -= fuelConsumption * Time.fixedDeltaTime;  
            }
            if (jetpackFuel <= 0)
            {
                jetpackFuel = 0;
                jetpackOn = false;
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
                    jetpackFuel += rechargeJetpack * Time.deltaTime;
                    
                    if(jetpackFuel >= maxFuel)
                    {
                        jetpackFuel = maxFuel;
                    }
                }
            }
            if(jetpackFuel <= 0)
            {
                timer += Time.deltaTime;
               
                if(timer > 0.5f)
                {
                    if (jetpackFuel < maxFuel)
                    {
                        jetpackFuel += rechargeJetpack * Time.deltaTime;
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
