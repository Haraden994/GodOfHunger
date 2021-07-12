using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyStats : CharacterStats
{
    public bool alive;

    protected override void Awake()
    {
        base.Awake();
        alive = true;
    }

    public override void Die()
    {
        base.Die();
        alive = false;
        GameManager.instance.EnemyDead(gameObject);

        // Death effect
        GetComponent<Animator>().SetTrigger("dead");
        GetComponent<EnemyController>().Die();

        //Destroy(gameObject, 2.0f);
    }
    
}
