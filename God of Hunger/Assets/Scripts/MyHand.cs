using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHand : OVRHand
{
    //public Transform handAnchor;
    //public GameObject handPlaceholder;

    public GameObject sideHandTools;

    private bool trackingLost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
