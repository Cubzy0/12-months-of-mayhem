using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int scoreValue = 100;
    [SerializeField] private GameObject deathEffect;

    [Header("Powerup Drop")]
    [SerializeField] private GameObject[] powerupPrefabs;          // Speed + Slow, etc.
    [SerializeField, Range(0f, 1f)] private float dropChance = 0.2f; // 20% chance for ANY powerup

    private int currentHealth;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        // Score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }

        // Death VFX
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Try to drop a powerup
        TryDropPowerup();

        Destroy(gameObject);
    }

    private void TryDropPowerup()
    {
        if (powerupPrefabs == null || powerupPrefabs.Length == 0)
            return;

        float roll = Random.value; // 0.0–1.0
        if (roll > dropChance)
            return;

        // Pick a random powerup from the list
        int index = Random.Range(0, powerupPrefabs.Length);
        GameObject chosen = powerupPrefabs[index];

        if (chosen != null)
        {
            Instantiate(chosen, transform.position, Quaternion.identity);
        }
    }
}