using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSelector : MonoBehaviour
{
    private ChargedGesture activePower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChargingPower()
    {
        if (activePower != null)
        {
            activePower.Pause();
        }
    }

    public void PowerCharged(ChargedGesture power)
    {
        if (activePower != null)
        {
            activePower.AnotherPowerCharged();
        }
        activePower = power;
    }

    public void ChargingInterrupted()
    {
        if (activePower != null)
        {
            activePower.Resume();
        }
    }

    public void PowerExpired()
    {
        activePower = null;
    }
}
