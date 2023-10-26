using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth1 : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    public Healthbar1 healthBar;
    public Healthbar1 healthBar2;

    public float showHealthBarTime = 2f;
    public float showhealthBarCurrentTime;

    public GameObject HealthBar2;


    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth);
        healthBar2.SetMaxHealth(maxHealth);

        showhealthBarCurrentTime = 0f;
    }

    void Update()
    {
        showhealthBarCurrentTime -= Time.deltaTime * 1f;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (showhealthBarCurrentTime >= 0f)
        {
            HealthBar2.SetActive(true);
        }
        else if (showhealthBarCurrentTime <= 0f)
        {
            HealthBar2.SetActive(false);
        }
    }

    public void TakeDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;

        healthBar.SetHealth(currentHealth);
        healthBar2.SetHealth(currentHealth);

        showhealthBarCurrentTime = showHealthBarTime;
    }
}
