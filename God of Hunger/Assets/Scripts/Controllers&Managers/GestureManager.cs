using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GestureDetector))]
public class GestureManager : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    
    private GestureDetector leftGestureDetector;
    private GestureDetector rightGestureDetector;

    // Start is called before the first frame update
    void Start()
    {
        GestureDetector[] detectors = GetComponents<GestureDetector>();

        foreach (var detector in detectors)
        {
            if (!detector.rightHand)
                leftGestureDetector = detector;
            else
                rightGestureDetector = detector;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
