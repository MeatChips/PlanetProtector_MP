using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetHealthManager : MonoBehaviour
{
    private int maxHealth = 1000;
    private int currentHealth;

    [SerializeField] private PlanetHealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxNumber(maxHealth);
    }

    private void Update()
    {
        if (currentHealth <= 0)
            Death();
        else
            return;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetNumber(currentHealth);
    }

    public void Death()
    {
        Destroy(gameObject);
        GameManager.Instance.gameEnded = true;
        GameManager.Instance.gamePaused = true;
    }
}
