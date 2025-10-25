using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<string, int> OnAmmoChanged;
    public event Action<WeaponSettings> OnWeaponCollected;
    public event Action<int> OnGrenadeCollected;
    private readonly Dictionary<string, int> _ammoStorage = new();
    private int _grenadeCount;

    public void AddWeapon(WeaponSettings settings)
    {
        OnWeaponCollected?.Invoke(settings);
    }

    public void AddAmmo(string itemID, int amount)
    {
        if (!_ammoStorage.ContainsKey(itemID))
            _ammoStorage[itemID] = 0;

        _ammoStorage[itemID] += amount;
        OnAmmoChanged?.Invoke(itemID, _ammoStorage[itemID]);
    }

    public bool TryUseAmmo(string itemID, int amount)
    {
        if (!_ammoStorage.TryGetValue(itemID, out int current) || current < amount)
            return false;

        _ammoStorage[itemID] -= amount;
        OnAmmoChanged?.Invoke(itemID, _ammoStorage[itemID]);
        return true;
    }

    public int GetAmmo(string itemID)
    {
        return _ammoStorage.TryGetValue(itemID, out int count) ? count : 0;
    }

    public void AddGrenades(int amount)
    {
        _grenadeCount += amount;
        OnGrenadeCollected?.Invoke(_grenadeCount);
    }

    public bool TryUseGrenade()
    {
        if (_grenadeCount <= 0)
            return false;

        _grenadeCount--;
        return true;
    }

    public int GetGrenadeCount() => _grenadeCount;

}
