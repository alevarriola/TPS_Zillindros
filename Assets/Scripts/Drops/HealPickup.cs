using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HealPickup : MonoBehaviour
{
    [SerializeField] private int healAmount = 15;

    // Para configurarlo por código desde el enemigo
    public void SetHealAmount(int amount)
    {
        healAmount = Mathf.Max(0, amount);
    }

    void Reset()
    {
        // Garantizar que el collider sea trigger
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buscamos un GunController en el player o hijos
        var ph = other.GetComponentInChildren<PlayerHealth>();
        if (ph == null) return;

        if (healAmount > 0)
        {
            ph.Heal(healAmount);
        }

        Destroy(gameObject);
    }
}