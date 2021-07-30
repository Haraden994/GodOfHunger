using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthBar : MonoBehaviour
{
    public float chipSpeed = 2f;
    public GameObject healthBarObject;
    public Image frontHealthBar;
    public Image backHealthBar;

    private CharacterStats stats;
    private float lerpTimer;

    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarObject.transform.LookAt(Camera.main.transform);
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = stats.currentHealth / stats.maxHealth;
        
        // Chip health bar reduction on damage taken
        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        // Chip health bar increase on damage healed
        if (fillFront < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void ResetLerpTimer()
    {
        lerpTimer = 0f;
    }
}
