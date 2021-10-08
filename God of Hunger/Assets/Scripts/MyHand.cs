using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHand : OVRHand
{
    //public Transform handAnchor;
    //public GameObject handPlaceholder;

    public GameObject sideHandTools;

    [HideInInspector] public Vector3 lastPos;
    [HideInInspector] public Quaternion lastRot;

    private bool trackingLost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        /*if (!IsTracked)
        {
            handPlaceholder.SetActive(true);
        }
        else
        {
            handPlaceholder.transform.position = handAnchor.position;
            handPlaceholder.transform.rotation = handAnchor.rotation;
            handPlaceholder.SetActive(false);
        }*/

        lastPos = transform.position;
        lastRot = transform.rotation;
        
        if (sideHandTools != null)
        {
            if (IsSystemGestureInProgress)
            {
                sideHandTools.SetActive(true);
            }
            else
            {
                sideHandTools.SetActive(false);
            }
        }
    }
}
