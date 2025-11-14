using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [Header("Projectile")]
    public GiftProjectile projectilePrefab;
    public Transform shootPoint;

    [Header("Shooting")]
    public float fireRate = 2f;          // shots per second
    public float projectileSpeed = 10f;
    public int projectileDamage = 1;
    public float detectionRadius = 10f;  // how far to search for enemies
    public LayerMask enemyLayerMask;

    private float _fireTimer;

    private void Update()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer < 1f / fireRate)
            return;

        Transform target = FindNearestEnemy();
        if (target == null)
            return;

        _fireTimer = 0f;
        ShootAt(target.position);
    }

    private void ShootAt(Vector3 targetPosition)
    {
        if (projectilePrefab == null || shootPoint == null) return;

        Vector2 dir = (targetPosition - shootPoint.position).normalized;

        GiftProjectile proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        proj.Init(dir, projectileSpeed, projectileDamage);

        // Optional: rotate sprite to face direction (if you want)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        proj.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            detectionRadius,
            enemyLayerMask
        );

        if (hits.Length == 0) return null;

        float bestDistSq = float.MaxValue;
        Transform bestTarget = null;

        foreach (var hit in hits)
        {
            float d = (hit.transform.position - transform.position).sqrMagnitude;
            if (d < bestDistSq)
            {
                bestDistSq = d;
                bestTarget = hit.transform;
            }
        }

        return bestTarget;
    }

    // Debug radius in Scene view
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}