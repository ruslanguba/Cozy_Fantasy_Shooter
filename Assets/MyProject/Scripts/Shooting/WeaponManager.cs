using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private List<WeaponSettings> weaponSettingsList;

    private readonly List<BaseWeapon> _weapons = new();
    private int _currentWeaponIndex;
    private BaseWeapon _currentWeapon;

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        // Создаём оружие из настроек
        foreach (var ws in weaponSettingsList)
        {
            BaseWeapon weapon = ws.isContinuous
                ? new ContinuousWeapon(ws, playerCamera)
                : new SingleShotWeapon(ws, playerCamera);

            _weapons.Add(weapon);
        }

        if (_weapons.Count > 0)
            _currentWeapon = _weapons[0];
    }

    private void Update()
    {
        if (_currentWeapon == null)
            return;

        _currentWeapon.UpdateWeapon(inputReader.IsFiringHeld());

        if (inputReader.GetReload())
            _currentWeapon.Reload();

        HandleWeaponSwitch();
    }

    private void HandleWeaponSwitch()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll > 0)
            SwitchWeapon((_currentWeaponIndex + 1) % _weapons.Count);
        else if (scroll < 0)
            SwitchWeapon((_currentWeaponIndex - 1 + _weapons.Count) % _weapons.Count);
    }

    private void SwitchWeapon(int index)
    {
        if (_weapons.Count == 0) return;

        _currentWeaponIndex = index;
        _currentWeapon = _weapons[index];
        Debug.Log($"Switched to: {weaponSettingsList[index].weaponName}");
    }
}
