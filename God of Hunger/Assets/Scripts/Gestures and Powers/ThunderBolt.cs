using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;

public class ThunderBolt : ChargedGesture
{
    [SerializeField] private GameObject hitPS;

    private List<GameObject> pooledPS = new List<GameObject>();
    
    private bool once = true;
    
    private CharacterStats targetStats;

    private int currentCharges;

    protected override void Start()
    {
        base.Start();
        GameObject instantiatedPS = Instantiate(hitPS);
        pooledPS.Add(instantiatedPS);
    }
    
    protected override void CheckTriggerAction()
    {
        base.CheckTriggerAction();
        
        if (currentTarget != null)
        {
            targetStats = currentTarget.GetComponentInParent<CharacterStats>();
            
            // Defocus target if dies
            if (targetStats.currentHealth <= 0.0f)
                rayTool._currInteractableCastedAgainst = null;

            if (once && magnitude >= velocityTriggerThreshold && dot >= velocityDirectionThreshold)
            {
                OnHit();
                targetStats.TakeDamage(PowersManager.instance.tbDamage);
                currentCharges--;
                once = false;
                StartCoroutine(Delay(0.4f));
            }

            if (currentCharges <= 0)
            {
                powerSelector.PowerExpired();
                if (rayTool != null)
                {
                    rayTool.targetAcquired = false;
                    rayTool._coneAngleDegrees = rayTool._defaultConeAngleDegrees;
                    rayTool._currInteractableCastedAgainst = null;
                }

                charged = false;
                chargedPS.gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        once = true;
    }

    private void OnHit()
    {
        GameObject instantiatedPS;
        
        if (pooledPS.Count > 0)
        {
            for (int i = 0; i < pooledPS.Count; i++)
            {
                if (!pooledPS[i].activeInHierarchy)
                {
                    pooledPS[i].transform.position = targetStats.transform.position;
                    pooledPS[i].SetActive(true);
                    return;
                }
            }
            
            instantiatedPS = Instantiate(hitPS, targetStats.transform.position, hitPS.transform.rotation);
            pooledPS.Add(instantiatedPS);
            instantiatedPS.SetActive(true);
        }
    }
    
    public override void OnGestureHold()
    {
        base.OnGestureHold();
        if (!charged)
        {
            if(!chargingPS.gameObject.activeSelf)
                chargingPS.gameObject.SetActive(true);
            
            charging += Time.deltaTime;
            if (charging >= PowersManager.instance.tbChargeTime)
            {
                powerSelector.PowerCharged(this);
                
                // if charged the ray tool must select the proper targets
                if (rayTool != null)
                {
                    rayTool.targetAcquired = true;
                    rayTool.targetType = "Enemy";
                    rayTool._coneAngleDegrees = 10.0f;
                }

                currentCharges = PowersManager.instance.tbCharges;
                chargingPS.gameObject.SetActive(false);
                chargedPS.gameObject.SetActive(true);
                charged = true;
                charging = 0.0f;
            }
        }
    }
}
