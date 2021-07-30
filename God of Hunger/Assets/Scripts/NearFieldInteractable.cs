using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;
using UnityEngine.Events;

public class NearFieldInteractable : MonoBehaviour
{
    public UnityEvent proximityEvent;
    public UnityEvent contactEvent;
    public UnityEvent actionEvent;
    public UnityEvent defaultEvent;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    private void InitiateEvent(InteractableStateArgs state)
    {
        if (state.NewInteractableState == InteractableState.ProximityState)
            proximityEvent.Invoke();
        else if (state.NewInteractableState == InteractableState.ContactState)
            contactEvent.Invoke();
        else if (state.NewInteractableState == InteractableState.ActionState)
            actionEvent.Invoke();
        else
            defaultEvent.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
