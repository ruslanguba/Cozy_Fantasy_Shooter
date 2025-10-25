using System;
using UnityEngine;

public abstract class Damagable : MonoBehaviour, IDamageable
{
    public event Action<float> OnHitReceived;
    public event Action OnDestroy;
    [SerializeField] protected float _hitPoints = 100f;
    [SerializeField] protected bool _destroyOnDeath = true;
    [SerializeField] protected AudioClip _destroySound;

    public virtual void TakeDamage(float damage)
    {
        if (_hitPoints <= 0)
            return;

        _hitPoints -= damage;
        OnHitReceived?.Invoke(_hitPoints);

        if (_hitPoints <= 0)
            Die();
        else
            OnHit();
    }

    protected virtual void OnHit() { }

    protected virtual void Die()
    {
        OnDestroy?.Invoke();
        if (_destroyOnDeath)
            Destroy(gameObject);

    }
}
