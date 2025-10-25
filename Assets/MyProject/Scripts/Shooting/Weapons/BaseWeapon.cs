using System;
using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseWeapon
{
    public Action<int> AmmoInMagazineñChanged;
    public Action Shoot;
    public Action StopShooting;
    public Action<Vector3, Vector3, bool> Hit;
    protected readonly WeaponSettings _settings;
    protected readonly Camera _camera;
    protected readonly Inventory _inventory;
    protected float _nextFireTime;
    protected int _currentAmmoInMagazine;
    protected bool _isReloading;
    protected float _reloadTimer;
    public WeaponSettings Settings => _settings;
    public int GetCurrentAmmo => _currentAmmoInMagazine;
    protected BaseWeapon(WeaponSettings settings, Camera camera, Inventory inventory)
    {
        _settings = settings;
        _camera = camera;
        _inventory = inventory;
        _currentAmmoInMagazine = settings.magazineSize;
    }

    protected bool TryShoot()
    {
        if (_isReloading) return false;
        if (_currentAmmoInMagazine <= 0)
        {
            Reload();
            return false;
        }

        _currentAmmoInMagazine--;
        AmmoInMagazineñChanged?.Invoke(_currentAmmoInMagazine);
        Shoot?.Invoke();
        FireRay();
        return true;
    }

    public virtual void Reload()
    {
        if (_isReloading) return;

        int neededAmmo = _settings.magazineSize - _currentAmmoInMagazine;
        int availableAmmo = _inventory.GetAmmo(_settings.ammoType);
        if (availableAmmo <= 0) return;

        int reloadAmount = Mathf.Min(neededAmmo, availableAmmo);
        _inventory.TryUseAmmo(_settings.ammoType, reloadAmount);

        _isReloading = true;
        _reloadTimer = _settings.reloadTime;
    }

    public void HandleReloadTimer()
    {
        if (_isReloading)
        {
            _reloadTimer -= Time.deltaTime;
            if (_reloadTimer <= 0)
            {
                _isReloading = false;
                _currentAmmoInMagazine = _settings.magazineSize;
                AmmoInMagazineñChanged?.Invoke(_currentAmmoInMagazine);
            }
        }
    }
    public abstract void UpdateWeapon(bool isHeld, bool justPressed);

    protected void FireRay()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _settings.range))
        {
            if (hit.collider.TryGetComponent(out IDamageable target))
                target.TakeDamage(_settings.damage);
            Hit?.Invoke(hit.point, hit.normal, true);
        }
        else
        {
            Hit?.Invoke(_camera.transform.position + _camera.transform.forward * _settings.range, Vector3.zero, false);
        }
    }
}
