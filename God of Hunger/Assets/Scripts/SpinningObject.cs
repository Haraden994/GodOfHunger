using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    public bool spin;
    public float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
    }

    public void Spin(bool s)
    {
        spin = s;
    }
}
