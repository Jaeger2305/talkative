using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent healthEmpty;
    public UnityEvent healthFull;
    public UnityEvent<int, int> healthChanged;
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;

    public void Start()
    {
        healthChanged.Invoke(health, 0);
    }

    /** Consume a specific amount - useful for triggering on a specific event */
    public void LoseHealth(int amount)
    {
        var healthBefore = health;
        health = System.Math.Clamp(health - amount, 0, maxHealth);

        // assume all the correct events were already triggered, and as a result just exit early on this event.
        // We were seeing this when the projectiles were entering a corpse's collider, and attempting to remove its health further
        // Probably semantically better to never call lose health, and remove the collider, or disable damage or something, but this is just quick.
        if (healthBefore == health) return;

        healthChanged.Invoke(health, amount); // careful here - amount will be positive in the event for both lose and gain health!
        if (health <= 0) healthEmpty.Invoke();
    }

    public void GainHealth(int amount)
    {
        health = System.Math.Clamp(health + amount, 0, maxHealth);
        healthChanged.Invoke(health, amount);
        if (health == maxHealth) healthFull.Invoke();
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        this.health = health;
    }
}