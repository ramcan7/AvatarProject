using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxController : MonoBehaviour
{
    [Header("HitboxConfiguration")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private LayerMask hitLayers;

    [Header("Debug")]
    [SerializeField] private bool showDebugGizmos = true;

    private Collider2D hitboxCollider;
    private HashSet<Collider2D> alreadyHitThisAttack = new HashSet<Collider2D>();

    private void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();
        hitboxCollider.isTrigger = true;

        hitboxCollider.enabled = false;
    }

    public void EnableHitbox()
    {
        alreadyHitThisAttack.Clear();
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
        alreadyHitThisAttack.Clear();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyHitThisAttack.Contains(other)) return;

        if ((1 << other.gameObject.layer & hitLayers) == 0) return;

        alreadyHitThisAttack.Add(other);

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Vector2 knockbackDir = (other.transform.position - transform.position).normalized;
            damageable.TakeDamage(damage, knockbackDir * knockbackForce);

            Debug.Log($"Hit {other.name} for {damage} damage");
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;

        Collider2D col = GetComponent<Collider2D>();
        if (col == null) return;

        Gizmos.color = hitboxCollider != null && hitboxCollider.enabled
            ? new Color(1, 0, 0, 0.5f)
            : new Color(1, 1, 0, 0.3f);

        if (col is BoxCollider2D box)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(box.offset, box.size);
        }
        else if (col is CircleCollider2D circle)
        {
            Gizmos.DrawSphere(transform.position + (Vector3)circle.offset, circle.radius);
        }
    }
}
