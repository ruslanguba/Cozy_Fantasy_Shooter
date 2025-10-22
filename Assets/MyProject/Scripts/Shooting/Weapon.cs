using UnityEngine;

public class Weapon
{
    //private WeaponSettings _settings;
    //private float _lastFireTime;
    //private int _currentAmmo;
    //private Camera _camera;
    //private ParticleSystem _particleSystem;

    //public Weapon(WeaponSettings settings, Camera camera, ParticleSystem hitParticle)
    //{
    //    _settings = settings;
    //    _camera = camera;
    //    _currentAmmo = _settings.magazineSize;
    //    _particleSystem = hitParticle;
    //}

    //public bool CanShoot()
    //{
    //    return Time.time - _lastFireTime >= _settings.fireRate && _currentAmmo > 0;
    //}

    //public void Shoot()
    //{
    //    if (!CanShoot()) return;

    //    _lastFireTime = Time.time;
    //    _currentAmmo--;

    //    // Raycast от центра экрана
    //    Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    //    if (Physics.Raycast(ray, out RaycastHit hit, _settings.range))
    //    {
    //        if(hit.collider.TryGetComponent<IDamageable>(out IDamageable damagable))
    //        {
    //            damagable.TakeDamage(_settings.damage);
    //        }
    //        _particleSystem.transform.position = hit.point;
    //        _particleSystem.Play();
    //    }
    //    // “ут можно добавить muzzle flash, звук и т.д.
    //}

    //public void Reload()
    //{
    //    _currentAmmo = _settings.magazineSize;
    //}

    //public int GetAmmo() => _currentAmmo;
}
