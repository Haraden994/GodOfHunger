using OculusSampleFramework;
using UnityEngine;

public class ChargedGesture : MonoBehaviour
{
    [SerializeField] protected ParticleSystem chargingPS;
    [SerializeField] protected ParticleSystem chargedPS;

    protected bool iamLeft;
    protected RayTool rayTool;
    protected MyHand hand;
    protected PowerSelector powerSelector;
    protected float charging;
    protected bool charged;
    protected bool paused;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hand = GetComponentInParent<MyHand>();
        powerSelector = GetComponentInParent<PowerSelector>();
        iamLeft = hand.gameObject.CompareTag("Left");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(rayTool == null && InteractableToolsInputRouter.Instance.toolsInitialized)
            rayTool = InteractableToolsInputRouter.Instance.GetRayTool(iamLeft);
        
        if(!paused && charged && hand.IsTracked)
            CheckTriggerAction();
    }

    protected virtual void CheckTriggerAction()
    {
        
    }

    public virtual void OnGestureRecognized()
    {
        if (!charged)
        {
            // Notify the power selector
            powerSelector.ChargingPower();
            //Debug.Log(gameObject.name + " charging!");
            charging = 0.0f;
            chargingPS.Play();
        }
    }

    public virtual void OnGestureHold()
    {
        //Debug.Log(gameObject.name + " holding!");
    }

    public virtual void OnGestureChange()
    {
        if (!charged)
        {
            chargingPS.Stop();
            charging = 0.0f;
            powerSelector.ChargingInterrupted();
            //Debug.Log(gameObject.name + " interrupted!");
        }
    }

    public void Pause()
    {
        chargedPS.Stop();
        paused = true;
    }

    public void Resume()
    {
        chargedPS.Play();
        paused = false;
    }

    public void AnotherPowerCharged()
    {
        charged = false;
        chargedPS.Stop();
        
        if (rayTool != null)
        {
            rayTool.targetType = null;
            rayTool._currInteractableCastedAgainst = null;
        }
    }
}