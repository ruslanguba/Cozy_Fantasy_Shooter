using System;
using Unity.Cinemachine;
using UnityEngine;

public abstract class BaseWeapon
{
    public Action<int> AmmoInMagazineñChanged;
    protected readonly WeaponSettings _settings;
    protected readonly Camera _camera;
    protected readonly Inventory _inventory;
    protected WeaponVFX _vfx;
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
        _vfx = new WeaponVFX(settings.muzzleFlashPrefab, settings.impactEffectPrefab, settings.muzzlePivot);
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
        _vfx.PlayMuzzleFlash(_settings.muzzlePivot);
        Debug.Log(_settings.muzzlePivot.position);
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
        _inventory.CanUseAmmo(_settings.ammoType, reloadAmount);

        _isReloading = true;
        _reloadTimer = _settings.reloadTime;
        Debug.Log($"Reloading {_settings.weaponName}...");
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
                Debug.Log($"{_settings.weaponName} reloaded!");
            }
        }
    }
    public abstract void UpdateWeapon(bool isHeld, bool justPressed);
    public virtual void SetAiming(bool isAiming) { }
    protected void FireRay()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _settings.range))
        {
            if (hit.collider.TryGetComponent(out IDamageable target))
                target.TakeDamage(_settings.damage);

            _vfx?.PlayImpact(hit.point, hit.normal);
            Debug.Log(hit.point);
        }
    }
}
