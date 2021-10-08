using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    private float damageMultiplier;
    private bool characterInside;

    public Transform center;
    
    [SerializeField] private ParticleSystem circles;
    [SerializeField] private ParticleSystem triggered;

    // Start is called before the first frame update
    void Start()
    {
        damageMultiplier = PowersManager.instance.msDamageMultiplierReduction;
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        circles.Play();
    }

    private IEnumerator DurationExpired(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        GameObject mainCharacter = GameManager.instance.mainCharacter;
        mainCharacter.GetComponent<MainCharacterController>().MagicShieldEnded();
        if(characterInside)
            mainCharacter.GetComponent<CharacterStats>().incomingDamageMultiplier.RemoveModifier(damageMultiplier);
        
        triggered.Stop();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCharacter"))
        {
            StartCoroutine(DurationExpired(PowersManager.instance.msDuration));
            characterInside = true;
            other.GetComponent<CharacterStats>().incomingDamageMultiplier.AddModifier(damageMultiplier);

            triggered.Play();
            circles.Stop();
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
