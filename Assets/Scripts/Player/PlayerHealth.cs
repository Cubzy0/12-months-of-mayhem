using UnityEngine;
using Unity.Cinemachine;   // for CinemachineImpulseSource

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;

    private int currentHealth;

    [Header("UI & FX")]
    [SerializeField] private HealthUI healthUI;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        currentHealth = maxHealth;

        // Auto-find HealthUI if not assigned
        if (healthUI == null)
            healthUI = FindAnyObjectByType<HealthUI>();
    }

    private void Start()
    {
        if (healthUI != null)
            healthUI.UpdateHearts(currentHealth, maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Player hit! HP: {currentHealth}/{maxHealth}");

        // Update hearts
        if (healthUI != null)
            healthUI.UpdateHearts(currentHealth, maxHealth);

        // Red flash
        if (damageFlash != null)
            damageFlash.TriggerFlash();

        // Camera shake
        if (impulseSource != null)
            impulseSource.GenerateImpulse();

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Player died!");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            // Fallback if there is no GameManager in the scene
            Time.timeScale = 0f;
        }
    }
}