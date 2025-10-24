using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : IDisposable
{
    public event Action<BaseWeapon> OnWeaponChanged;

    private readonly List<BaseWeapon> _weapons = new();

    private readonly Camera _camera;
    private readonly Inventory _inventory;
    private int _currentIndex;

    public BaseWeapon CurrentWeapon => _weapons.Count > 0 ? _weapons[_currentIndex] : null;

    public WeaponHolder(Camera camera, Inventory inventory)
    {
        _camera = camera;
        _inventory = inventory;
    }

    public void BindToInventory()
    {
        _inventory.WeaponCollected += AddWeapon;
    }

    public void UnbindFromInventory()
    {
        _inventory.WeaponCollected -= AddWeapon;
    }

    public void AddWeapon(WeaponSettings settings)
    {
        if (settings == null) return;
        if (_weapons.Exists(w => w.Settings.weaponName == settings.weaponName))
        {
            Debug.Log($"Weapon {settings.weaponName} already exists.");
            return;
        }

        BaseWeapon weapon = settings.isContinuous
            ? new ContinuousWeapon(settings, _camera, _inventory)
            : new SingleShotWeapon(settings, _camera, _inventory);

        _inventory.AddAmmo(settings.ammoType, 100);
        _weapons.Add(weapon);

        Debug.Log($"Weapon added: {settings.weaponName}");

        // Автоматически активируем, если первое
        if (_weapons.Count == 1)
            SetCurrentWeapon(0);
    }

    public void NextWeapon() => SwitchWeapon(_currentIndex + 1);
    public void PrevWeapon() => SwitchWeapon(_currentIndex - 1);

    private void SwitchWeapon(int newIndex)
    {
        if (_weapons.Count == 0) return;
        _currentIndex = (newIndex + _weapons.Count) % _weapons.Count;
        OnWeaponChanged?.Invoke(CurrentWeapon);
    }

    private void SetCurrentWeapon(int index)
    {
        if (index < 0 || index >= _weapons.Count) return;
        _currentIndex = index;
        OnWeaponChanged?.Invoke(CurrentWeapon);
    }

    public void Dispose()
    {
        _inventory.WeaponCollected -= AddWeapon;
    }
}
