using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth { get; private set; }
    
    public Stat damage;
    public Stat attackSpeed;
    public Stat armor;
    public Stat incomingDamageMultiplier;

    private HealthBar healthBar;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        healthBar = GetComponent<HealthBar>();
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = damage * incomingDamageMultiplier.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        if (healthBar != null)
        {
            healthBar.ResetLerpTimer();
        }
        
        if (damage > currentHealth)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= damage;
        }

        Debug.Log(transform.name + " takes " + damage + " damage.");
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (healthBar != null)
        {
            StartCoroutine(HideHealthBar());
        }
        // Ready for override.
        Debug.Log(transform.name + " died.");
    }

    public IEnumerator HideHealthBar()
    {
        yield return new WaitForSeconds(1f);
        healthBar.healthBarObject.SetActive(false);
    }
    
}
