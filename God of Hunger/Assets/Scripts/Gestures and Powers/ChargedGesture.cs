using OculusSampleFramework;
using UnityEngine;

public class ChargedGesture : MonoBehaviour
{
    [SerializeField] protected ParticleSystem chargingPS;
    [SerializeField] protected ParticleSystem chargedPS;
    
    [SerializeField] protected Transform facing;
    [SerializeField] protected float velocityDirectionThreshold = 0.4f;
    [SerializeField] protected float velocityTriggerThreshold = 1.2f;
    
    protected Vector3 velocity;
    protected float dot;
    protected float magnitude;
    protected Interactable currentTarget;

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
        velocity = (hand.transform.position - hand.lastPos) / Time.fixedDeltaTime;
        dot = Vector3.Dot(facing.forward, velocity.normalized);
        magnitude = velocity.magnitude;
        
        currentTarget = rayTool._currInteractableCastedAgainst;
    }

    public virtual void OnGestureRecognized()
    {
        if (!charged)
        {
            // Notify the power selector
            powerSelector.ChargingPower();
            //Debug.Log(gameObject.name + " charging!");
            charging = 0.0f;
            chargingPS.gameObject.SetActive(true);
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
            chargingPS.gameObject.SetActive(false);
            charging = 0.0f;
            powerSelector.ChargingInterrupted();
            //Debug.Log(gameObject.name + " interrupted!");
        }
    }

    public void Pause()
    {
        chargedPS.gameObject.SetActive(false);
        paused = true;
    }

    public void Resume()
    {
        chargedPS.gameObject.SetActive(true);
        paused = false;
    }

    public void AnotherPowerCharged()
    {
        paused = false;
        charged = false;
        chargedPS.gameObject.SetActive(false);
        
        if (rayTool != null)
        {
            rayTool._currInteractableCastedAgainst = null;
        }
    }
}