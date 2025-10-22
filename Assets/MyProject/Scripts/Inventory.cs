using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action<string, int> OnAmmoChanged;
    private readonly Dictionary<string, int> _ammoStorage = new();
    private int _grenadeCount;

    public void AddAmmo(string itemID, int amount)
    {
        if (!_ammoStorage.ContainsKey(itemID))
            _ammoStorage[itemID] = 0;

        _ammoStorage[itemID] += amount;
        OnAmmoChanged(itemID, _ammoStorage[itemID]);
        Debug.Log($"Added {amount} {itemID} ammo. Now: {_ammoStorage[itemID]}");
    }

    public bool CanUseAmmo(string itemID, int amount)
    {
        if (!_ammoStorage.TryGetValue(itemID, out int current) || current < amount)
            return false;

        _ammoStorage[itemID] -= amount;
        OnAmmoChanged(itemID, _ammoStorage[itemID]);
        return true;
    }

    public int GetAmmo(string itemID)
    {
        return _ammoStorage.TryGetValue(itemID, out int count) ? count : 0;
    }

    public void AddGrenades(int amount)
    {
        _grenadeCount += amount;
        Debug.Log($"Added {amount} grenades. Now: {_grenadeCount}");
    }

    public bool CanUseGrenade()
    {
        if (_grenadeCount <= 0)
            return false;

        _grenadeCount--;
        return true;
    }

    public int GetGrenadeCount() => _grenadeCount;

}
