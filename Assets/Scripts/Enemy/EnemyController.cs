using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float stoppingDistance = 0f;
    [SerializeField] private string playerTag = "Player";

    private Rigidbody2D rb;
    private Transform target;

    // Speed modifier support
    private float baseMoveSpeed;
    private bool isSpeedModified;
    private Coroutine speedModifierRoutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyController: No Player found with tag '" + playerTag + "'.");
        }

        baseMoveSpeed = moveSpeed; // remember original speed
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 dir = (target.position - transform.position);
        float distance = dir.magnitude;

        // Optional: stop moving when very close
        if (stoppingDistance > 0f && distance <= stoppingDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        dir.Normalize();
        rb.linearVelocity = dir * moveSpeed;
    }

    private void OnDisable()
    {
        // Avoid "ghost" movement when disabled/destroyed
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    // ---- SPEED MODIFIER API (used by slow powerup) ----
    public void ApplySpeedModifier(float multiplier, float duration)
    {
        if (speedModifierRoutine != null)
        {
            StopCoroutine(speedModifierRoutine);
        }

        speedModifierRoutine = StartCoroutine(SpeedModifierCoroutine(multiplier, duration));
    }

    private IEnumerator SpeedModifierCoroutine(float multiplier, float duration)
    {
        isSpeedModified = true;
        moveSpeed = baseMoveSpeed * multiplier;

        yield return new WaitForSeconds(duration);

        moveSpeed = baseMoveSpeed;
        isSpeedModified = false;
        speedModifierRoutine = null;
    }
}