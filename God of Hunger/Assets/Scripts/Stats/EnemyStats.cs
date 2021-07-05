using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyStats : CharacterStats
{

    public override void Die()
    {
        base.Die();
        
        // Death effect

        Destroy(gameObject, 2.0f);
    }
    
}
