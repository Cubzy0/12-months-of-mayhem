using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float deathDelay = 0.5f;

    private int currentHealth;
    private HealthUI healthUI;
    [SerializeField] private DamageFlash damageFlash;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthUI = FindAnyObjectByType<HealthUI>();
    }

    private void Start()
    {
        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player hit! HP: " + currentHealth + "/" + maxHealth);

        if (healthUI != null)
        {
            healthUI.UpdateHearts(currentHealth, maxHealth);
        }

        if (damageFlash != null)
        {
            damageFlash.TriggerFlash();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        Invoke(nameof(ReloadScene), deathDelay);
    }

    private void ReloadScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }
}
