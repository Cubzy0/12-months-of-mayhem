using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ApplySpeedBoost(speedMultiplier, duration);
        }

        // TODO: play pickup VFX/SFX here

        Destroy(gameObject);
    }
}