using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterStats))]
public class MainCharacterController : MonoBehaviour
{
    private Transform goal;
    private GameObject targetEnemy;
    private CharacterStats targetStats;
    private CharacterCombat combat;
    private NavMeshAgent agent;

    private bool enemyTargeted;

    // Start is called before the first frame update
    void Start()
    {
        goal = GameManager.instance.actualGoal;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.enemies.Count > 0)
        {
            CombatBehaviour();
        }
        else
        {
            ReachGoal();
        }
    }

    private void CombatBehaviour()
    {
        // Seek the closest enemy and rush towards, then attack
        if (!enemyTargeted)
        {
            targetEnemy = SearchForEnemy();
        }
        else
        {
            Vector3 targetEnemyPosition = targetEnemy.transform.position;

            float distanceFromEnemy = Vector3.Distance(targetEnemyPosition, transform.position);

            agent.SetDestination(targetEnemyPosition);
            if (distanceFromEnemy <= agent.stoppingDistance)
            {
                combat.Attack(targetStats);

                FaceTarget(targetEnemyPosition);
            }

            if (targetStats.currentHealth <= 0)
            {
                enemyTargeted = false;
            }
        }
    }

    private GameObject SearchForEnemy()
    {
        float minDistance = Mathf.Infinity;
        GameObject closestEnemy = null;
        List<GameObject> enemies = GameManager.instance.enemies;
        Vector3 myPosition = transform.position;
        
        foreach (var enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - myPosition;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < minDistance)
            {
                closestEnemy = enemy;
                minDistance = currentDistance;
            }
        }

        enemyTargeted = true;
        targetStats = closestEnemy.GetComponent<CharacterStats>();
        return closestEnemy;
    }
    
    private void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    
    private void ReachGoal()
    {
        agent.SetDestination(goal.position);
    }
}
