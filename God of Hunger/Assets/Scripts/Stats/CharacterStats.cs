using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth { get; private set; }
    
    public Stat damage;
    public Stat attackSpeed;
    public Stat armor;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);
        
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Ready for override.
        Debug.Log(transform.name + " died.");
    }
    
}
