using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDummy : MonoBehaviour, IDamageable
{
    [Header("Vida")]
    public float maxHealth = 50f;
    public GameObject hitVfxPrefab; // opcional
    float _hp;

    void Awake() => _hp = maxHealth;

    public void TakeDamage(float amount, Vector3 hitPoint, Vector3 hitNormal)
    {
        _hp -= amount;

        if (hitVfxPrefab)
        {
            var vfx = Instantiate(hitVfxPrefab, hitPoint, Quaternion.LookRotation(hitNormal));
            Destroy(vfx, 2f);
        }

        if (_hp <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

