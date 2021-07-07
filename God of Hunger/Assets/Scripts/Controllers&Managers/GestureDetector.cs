using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public struct Gesture
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
    public UnityEvent onHold;
    public UnityEvent onChange;
}

public class GestureDetector : MonoBehaviour
{
    public bool recMode = true;
    public bool rightHand;
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<Gesture> gestures;
    
    private List<OVRBone> fingerBones;
    private Gesture previousGesture;
    private bool bonesInitialized;
    private bool gestureChange;

    private Gesture currentGesture;
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(BonesDelay(2.5f, InitializeBones));
        
        previousGesture = new Gesture();
    }

    private IEnumerator BonesDelay(float delay, Action toDo)
    {
        yield return new WaitForSeconds(delay);
        toDo.Invoke();
    }

    private void InitializeBones()
    {
        fingerBones = new List<OVRBone>(skeleton.Bones);

        bonesInitialized = true;
        Debug.Log("Bones Initialized!");
    }

    // Update is called once per frame
    private void Update()
    {
        if (bonesInitialized)
        {
            if (recMode)
            {
                if (!rightHand)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                        Save();
                }
                else
                    if (Input.GetKeyDown(KeyCode.RightControl))
                        Save();
            }

            currentGesture = Recognize();
            // check if a saved gesture has been recognized
            bool hasRecognized = !currentGesture.Equals(new Gesture());

            if (hasRecognized)
            {
                if (!currentGesture.Equals(previousGesture))
                {
                    gestureChange = true;
                    Debug.Log("New Gesture Found: " + currentGesture.name);
                    previousGesture = currentGesture;
                    currentGesture.onRecognized.Invoke();
                }
                else
                {
                    Debug.Log("OnHold");
                    currentGesture.onHold.Invoke();
                }
            }
            else
            {
                if (gestureChange)
                {
                    Debug.Log("OnChange");
                    gestureChange = false;
                    previousGesture.onChange.Invoke();
                    previousGesture = new Gesture();
                }
            }
        }
    }

    private void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach (var bone in fingerBones)
        {
            // finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }

        g.fingerDatas = data;
        gestures.Add(g);
        Debug.Log("New Gesture Added!");
    }

    private Gesture Recognize()
    {
        Gesture newGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures)
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for (int i = 0; i < fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
                if (distance > threshold)
                {
                    isDiscarded = true;
                    break;
                }

                sumDistance += distance;
            }

            if (!isDiscarded && sumDistance < currentMin)
            {
                currentMin = sumDistance;
                newGesture = gesture;
            }
        }

        return newGesture;
    }

    public void HoldingHandDebug()
    {
        Debug.Log("Holding Gesture " + currentGesture);
    }
}
