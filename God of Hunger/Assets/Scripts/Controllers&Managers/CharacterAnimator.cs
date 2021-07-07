using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    public AnimatorOverrideController overrideController;

    private const float locomotionAnimationSmoothTime = .1f;
    
    private Animator animator;
    private NavMeshAgent agent;
    private CharacterCombat combat;
    private CharacterStats stats;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        
        if(overrideController == null)
            overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    void Update()
    {
        float speedPercent = agent.speed != 0 ? agent.velocity.magnitude / agent.speed : 0.0f;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);
        animator.SetFloat("locomotionSpeed", agent.speed);
    }

    private void OnAttack()
    {
        animator.SetFloat("attackSpeed", stats.attackSpeed.GetValue());
        animator.SetTrigger("attack");
        
        int attackIndex = Random.Range(0, defaultAttackAnimSet.Length);
        overrideController[replaceableAttackAnim.name] = defaultAttackAnimSet[attackIndex];
    }

    public float GetAttackClipDuration()
    {
        return overrideController[replaceableAttackAnim.name].length;
    }
}
