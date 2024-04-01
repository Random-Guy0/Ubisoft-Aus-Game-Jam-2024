using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthBase : MonoBehaviour, IDamageable
{ 
    public int Health { get; private set; }

    [SerializeField] private int maxHealth;

    private void Awake()
    {
        Health = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        
        OnTakeDamage?.Invoke(damageAmount);

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public event IDamageable.OnTakeDamageHandler OnTakeDamage;

    protected abstract void Die();
}
