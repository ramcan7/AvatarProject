using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    public bool IsDead => currentHealth < 0;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        currentHealth -= damage;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(knockback, ForceMode2D.Impulse);

        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
