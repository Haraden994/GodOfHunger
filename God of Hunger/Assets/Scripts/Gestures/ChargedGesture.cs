using OculusSampleFramework;
using UnityEngine;


public class ChargedGesture : MonoBehaviour
{
    [SerializeField] protected ParticleSystem chargingPS;
    [SerializeField] protected ParticleSystem chargedPS;

    protected bool iamLeft;
    protected RayTool rayTool;
    protected MyHand hand;
    protected float charging;
    protected bool charged;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        hand = GetComponentInParent<MyHand>();
        iamLeft = hand.gameObject.CompareTag("Left");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(rayTool == null && InteractableToolsInputRouter.Instance.toolsInitialized)
            rayTool = InteractableToolsInputRouter.Instance.GetRayTool(iamLeft);
        
        if(charged && hand.IsTracked)
            CheckTriggerAction();
    }

    protected virtual void CheckTriggerAction()
    {
        
    }

    public virtual void OnGestureRecognized()
    {
        if (!charged)
        {
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
            //Debug.Log(gameObject.name + " interrupted!");
        }
    }
}