using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowersManager : MonoBehaviour
{
    
    #region Singleton

    public static PowersManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion
    
    [Header("Thunder Bolt")] 
    public int tbCharges;
    public float tbChargeTime;
    public float tbDamage;

    [Header("Magic Shield")] 
    public int msCharges;
    public float msChargeTime;
    public float msDuration;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
