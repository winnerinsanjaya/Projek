using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    // Properti tambahan untuk mendapatkan current health
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Tambahkan logika kematian enemy di sini, misalnya memanggil animasi atau menghancurkan GameObject
        Destroy(gameObject);
    }
}
