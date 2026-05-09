using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject face;
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

        StartCoroutine(DamageRoutine());

        if (IsDead)
        {
            Die();
        }
    }


    private IEnumerator DamageRoutine()
    {
        face.SetActive(true);
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = new Color(1f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.35f);
        spriteRend.color = new Color(1f, 1f, 1f, 1f);
        face.SetActive(false);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
