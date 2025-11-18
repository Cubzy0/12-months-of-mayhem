using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float deathDelay = 0.5f;

    private int currentHealth;

    [SerializeField] private HealthUI healthUI;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        currentHealth = maxHealth;

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
        Debug.Log("Player hit! HP: " + currentHealth + "/" + maxHealth);

        if (healthUI != null)
            healthUI.UpdateHearts(currentHealth, maxHealth);

        if (damageFlash != null)
            damageFlash.TriggerFlash();

        if (impulseSource != null)
            impulseSource.GenerateImpulse();

        if (currentHealth <= 0)
            Die();
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
