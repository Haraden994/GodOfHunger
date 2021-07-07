using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyGrabbable : OVRGrabbable
{
    private EnemyController enemyController;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        enemyController = GetComponent<EnemyController>();
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        enemyController.Grabbed(true);
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        enemyController.Grabbed(false);
    }
}
