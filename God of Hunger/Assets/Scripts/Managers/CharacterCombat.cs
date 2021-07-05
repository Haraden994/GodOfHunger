using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;

    public event System.Action OnAttack;
    
    private CharacterStats myStats;
    private CharacterStats opponentStats;
    private CharacterAnimator chAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
        chAnimator = GetComponent<CharacterAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            opponentStats = targetStats;
            if (OnAttack != null)
                OnAttack();

            // The attack speed is based on the animation length so higher attack speed wont make animations overlap
            attackCooldown = chAnimator.GetAttackClipDuration() / myStats.attackSpeed.GetValue();
        }
    }

    public void AttackHitEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
    }
}
