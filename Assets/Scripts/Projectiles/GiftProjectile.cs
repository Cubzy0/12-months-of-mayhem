using UnityEngine;

public class GiftProjectile : MonoBehaviour
{
    [HideInInspector] public float speed = 10f;
    [HideInInspector] public int damage = 1;

    [SerializeField] private float lifeTime = 3f;

    private Vector2 _direction;

    public void Init(Vector2 direction, float newSpeed, int newDamage)
    {
        _direction = direction.normalized;
        speed = newSpeed;
        damage = newDamage;
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * speed * Time.deltaTime);
    }

   private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Enemy"))
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
}