using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 10; // Amount of health this potion restores

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.name);
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable != null && damageable.IsAlive)
        {
            // Restore health without exceeding max health
            damageable.Health = Mathf.Min(damageable.Health + healthAmount, damageable.MaxHealth);
            Debug.Log("Potion picked up, restoring health. New health: " + damageable.Health);
            Destroy(gameObject); // Destroy the potion after picking it up
            Debug.Log("Potion object destroyed");
        }
        else
        {
            Debug.Log("No damageable component found or the object is not alive.");
        }
    }
}
