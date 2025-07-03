using System;
using UnityEngine;

public class HealthSystem
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDead;

    private float _health, _maxHealth;

    public float Health { get => _health; private set => _health = value; }

    public HealthSystem (float maxHealth)
    {
        _maxHealth = maxHealth;
        Health = _maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;

        OnDamaged?.Invoke(this, EventArgs.Empty);

        Debug.Log("Current Health: " + Health);

        if (Health <= 0)
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }
}

