using UnityEngine;

public class Grenade : ThrowableBase
{
    [SerializeField] private GameObject _explosionVFX;
    private float _damageRadius;
    private float _damage;

    public override void Initialize(ThrowableSettings settings)
    {
        _damageRadius = settings.damageRadius;
        _damage = settings.damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    private void Explode()
    {
        if (_explosionVFX != null)
        {
            GameObject explosionEffect = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
            explosionEffect.GetComponent<ParticleSystem>().Play();
            Destroy(explosionEffect, 1);
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
