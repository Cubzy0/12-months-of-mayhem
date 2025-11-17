using UnityEngine;

public class DamagePlayerOnContact : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float hitCooldown = 0.5f;

    private float lastHitTime = -999f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryDamage(other);
    }

    private void TryDamage(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (Time.time < lastHitTime + hitCooldown) return;

        lastHitTime = Time.time;

        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}