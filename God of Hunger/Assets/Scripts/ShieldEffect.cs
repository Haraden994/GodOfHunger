using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    private float damageMultiplier;
    private bool characterInside;
    private ParticleSystem ps;

    [SerializeField] private ParticleSystem stars;

    // Start is called before the first frame update
    void Start()
    {
        damageMultiplier = PowersManager.instance.msDamageMultiplierReduction;
    }

    private void OnEnable()
    {
        StartCoroutine(DurationExpired(PowersManager.instance.msDuration));
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = true;
        var secondary = stars.main;
        secondary.loop = true;
    }

    private IEnumerator DurationExpired(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(characterInside)
            GameManager.instance.mainCharacter.GetComponent<CharacterStats>().incomingDamageMultiplier.RemoveModifier(damageMultiplier);
        var main = ps.main;
        main.loop = false;
        var secondary = stars.main;
        secondary.loop = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            characterInside = true;
            other.GetComponent<CharacterStats>().incomingDamageMultiplier.AddModifier(damageMultiplier);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            characterInside = false;
            other.GetComponent<CharacterStats>().incomingDamageMultiplier.RemoveModifier(damageMultiplier);
        }
    }
}
