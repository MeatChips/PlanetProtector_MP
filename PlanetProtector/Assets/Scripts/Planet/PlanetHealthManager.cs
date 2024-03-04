using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHealthManager : MonoBehaviour
{
    private int maxHealth = 1000;
    public int currentHealth;

    public PlanetHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxNumber(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetNumber(currentHealth);

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
