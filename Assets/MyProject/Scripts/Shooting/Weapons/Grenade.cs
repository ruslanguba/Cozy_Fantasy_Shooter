using UnityEngine;

public class Grenade : ThrowableBase
{
    private VFXEffectController _effect;
    protected AudioClip _destroySound;
    private float _damageRadius;
    private float _damage;

    public override void Initialize(ThrowableSettings settings)
    {
        _damageRadius = settings.damageRadius;
        _damage = settings.damage;
        _effect = settings.effect;
        _destroySound = settings.destroySound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        if (_effect != null)
        {
            VFXEffectController effect = Instantiate(_effect, transform.position, Quaternion.identity);
            effect.SetEffect(_destroySound);
            effect.PlayEffect();
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, _damageRadius);
        foreach (var c in hits)
        {
            if (c.TryGetComponent<IDamageable>(out var target))
                target.TakeDamage(_damage);

            if (c.attachedRigidbody != null)
                c.attachedRigidbody.AddExplosionForce(500f, transform.position, _damageRadius);
        }

        Destroy(gameObject);
    }
}
