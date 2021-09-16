using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : ChargedGesture
{
    [SerializeField] private GameObject magicShield;

    private GameObject instantiatedShield;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        instantiatedShield = Instantiate(magicShield);
    }

    protected override void CheckTriggerAction()
    {
        base.CheckTriggerAction();

        if (currentTarget != null)
        {
            if (magnitude >= velocityTriggerThreshold && dot >= velocityDirectionThreshold)
            {
                Vector3 targetPosition = currentTarget.transform.position;
                instantiatedShield.transform.position = new Vector3(targetPosition.x, instantiatedShield.transform.position.y, targetPosition.z);
                instantiatedShield.SetActive(true);
                
                powerSelector.PowerExpired();
                if (rayTool != null)
                {
                    rayTool.targetAcquired = false;
                    rayTool._coneAngleDegrees = rayTool._defaultConeAngleDegrees;
                    rayTool._currInteractableCastedAgainst = null;
                    
                    charged = false;
                    chargedPS.gameObject.SetActive(false);
                }
            }
        }
    }

    public override void OnGestureHold()
    {
        base.OnGestureHold();
        if (!charged)
        {
            if(!chargingPS.isPlaying)
                chargingPS.gameObject.SetActive(true);
            
            charging += Time.deltaTime;
            if (charging >= PowersManager.instance.msChargeTime)
            {
                powerSelector.PowerCharged(this);
                
                // if charged the ray tool must select the proper targets
                if (rayTool != null)
                {
                    rayTool.targetAcquired = true;
                    rayTool.targetType = "Ground";
                    rayTool._coneAngleDegrees = 5.0f;
                }
                
                chargingPS.gameObject.SetActive(false);
                chargedPS.gameObject.SetActive(true);
                charged = true;
                charging = 0.0f;
            }
        }
    }
}
