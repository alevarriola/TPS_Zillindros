using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDummy : MonoBehaviour, IDamageable
{
    [Header("Vida")]
    public float maxHealth = 50f;
    public GameObject hitVfxPrefab; // opcional
    float _hp;

    [Header("Drop de munición")]
    [Range(0f, 1f)]
    public float dropChace = 0.3f; // probabilidad de drop
    public int minAmmoDrop = 10;
    public int maxAmmoDrop = 30;
    public GameObject ammoPickupPrefab; // tu AmmoDrop prefab

    [Header("Drop de vida")]
    [Range(0f, 1f)]
    public float dropChacehp = 0.3f; // probabilidad de drop
    public int minhpDrop = 10;
    public int maxhpDrop = 30;
    public GameObject healPickupPrefab; // tu healDrop prefab

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
        TryDropAmmo();
        TryDropHeal();
        Destroy(gameObject);
    }

    void TryDropAmmo()
    {
        // Tirar un dado
        float random = Random.value;
        if (random > dropChace) return;

        int amount = Random.Range(minAmmoDrop, maxAmmoDrop+1);

        // Crear el pickup
        var pickups = Instantiate(ammoPickupPrefab, transform.position, Quaternion.identity);

        var ammoPickup = pickups.GetComponent<AmmoPickup>();
        if (ammoPickup != null)
        {
            ammoPickup.SetAmmoAmount(amount);
        }
    }

    void TryDropHeal()
    {
        // Tirar un dado
        float random = Random.value;
        if (random > dropChacehp) return;

        int amount = Random.Range(minhpDrop, maxhpDrop + 1);

        // Crear el pickup
        var pickup = Instantiate(healPickupPrefab, transform.position, Quaternion.identity);

        var healPickup = pickup.GetComponent<HealPickup>();
        if (healPickup != null)
        {
            healPickup.SetHealAmount(amount);
        }
    }
}

