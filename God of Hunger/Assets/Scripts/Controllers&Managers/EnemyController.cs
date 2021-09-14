using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterStats))]
public class EnemyController : MonoBehaviour
{
    public GameObject interactable;
    public float lookRadius = 10f;

    private Transform target;
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats targetStats;
    private bool isGrabbed;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.instance.mainCharacter.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        targetStats = target.GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.mainCharacterActive && !isDead)
        {
            ReachMinimumDistance();
        }
    }

    protected virtual void ReachMinimumDistance()
    {
        // Distance to the target
        float distance = Vector3.Distance(target.position, transform.position);
        
        // If inside the lookRadius
        if (distance <= lookRadius)
        {
            // Move towards the target
            agent.SetDestination(target.position);
            
            // If within attacking distance
            if (distance <= agent.stoppingDistance)
            {
                // Attack target
                combat.Attack(targetStats);

                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    public void Grabbed(bool grabbed)
    {
        isGrabbed = grabbed;
        // Ragdoll effect
    }

    public void Die()
    {
        interactable.SetActive(false);
        isDead = true;
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
