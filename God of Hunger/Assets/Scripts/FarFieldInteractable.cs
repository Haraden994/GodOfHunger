using System.Collections;
using System.Collections.Generic;
using OculusSampleFramework;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

public class FarFieldInteractable : MonoBehaviour
{
    [SerializeField] private SelectionCylinder _selectionCylinder;
    
    [SerializeField] private UnityEvent actionEvent;
    [SerializeField] private UnityEvent defaultEvent;

    private InteractableTool _toolInteractingWithMe;
    
    // Start is called before the first frame update
    void Awake()
    {
        Assert.IsNotNull(_selectionCylinder);
        GetComponent<ButtonController>().InteractableStateChanged.AddListener(InitiateEvent);
    }

    private void InitiateEvent(InteractableStateArgs obj)
    {
        bool inActionState = obj.NewInteractableState == InteractableState.ActionState;

        _toolInteractingWithMe = obj.NewInteractableState > InteractableState.Default ?
            obj.Tool : null;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_toolInteractingWithMe == null)
        {
            _selectionCylinder.CurrSelectionState = SelectionCylinder.SelectionState.Off;
        }
        else
        {
            _selectionCylinder.CurrSelectionState = (
                _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDown ||
                _toolInteractingWithMe.ToolInputState == ToolInputState.PrimaryInputDownStay)
                ? SelectionCylinder.SelectionState.Highlighted
                : SelectionCylinder.SelectionState.Selected;
        }
    }
}
