using UnityEngine;

public class PowerUpSlowEnemies : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f; // 50% speed
    [SerializeField] private float duration = 4f;         // seconds

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Find all active enemies
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var enemy in enemies)
        {
            enemy.ApplySpeedModifier(slowMultiplier, duration);
        }

        // TODO: add VFX/SFX here if you like

        Destroy(gameObject);
    }
}