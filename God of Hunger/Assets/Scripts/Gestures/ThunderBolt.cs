using System.Collections;
using OculusSampleFramework;
using UnityEngine;


public class ThunderBolt : ChargedGesture
{
    [SerializeField] private Transform facing;
    [SerializeField] private float velocityDirectionThreshold = 0.4f;
    [SerializeField] private float velocityTriggerThreshold = 1.2f;

    private Vector3 velocity;
    private float dot;
    private float magnitude;
    private bool once = true;
    
    private CharacterStats targetStats;

    private int currentCharges;

    protected override void CheckTriggerAction()
    {
        velocity = (hand.transform.position - hand.lastPos) / Time.fixedDeltaTime;
        dot = Vector3.Dot(facing.forward, velocity.normalized);
        magnitude = velocity.magnitude;

        Interactable currentTarget = rayTool._currInteractableCastedAgainst;
        if (currentTarget != null)
        {
            targetStats = currentTarget.GetComponentInParent<CharacterStats>();
            
            // Defocus target if dies
            if (targetStats.currentHealth <= 0.0f)
                rayTool._currInteractableCastedAgainst = null;

            if (once && magnitude >= velocityTriggerThreshold && dot >= velocityDirectionThreshold)
            {
                targetStats.TakeDamage(PowersManager.instance.tbDamage);
                currentCharges--;
                once = false;
                StartCoroutine(Delay(0.4f));
            }

            if (currentCharges <= 0)
            {
                if (rayTool != null)
                {
                    rayTool.targetType = null;
                    rayTool._currInteractableCastedAgainst = null;
                }

                charged = false;
                chargedPS.Stop();
            }
        }
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        once = true;
    }
    
    public override void OnGestureHold()
    {
        base.OnGestureHold();
        if (!charged)
        {
            if(!chargingPS.isPlaying)
                chargingPS.Play();
            
            charging += Time.deltaTime;
            if (charging >= PowersManager.instance.tbChargeTime)
            {
                // if charged the ray tool must select the proper targets
                if(rayTool != null)
                    rayTool.targetType = "Enemy";
                    
                currentCharges = PowersManager.instance.tbCharges;
                chargingPS.Stop();
                chargedPS.Play();
                charged = true;
                charging = 0.0f;
            }
        }
    }
}
