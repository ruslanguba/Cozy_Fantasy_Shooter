using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Action<BaseWeapon> WeaponChanged;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private CinemachineCamera _cinemaCamera;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private List<WeaponSettings> weaponSettingsList;
    [SerializeField] private Inventory inventory;
    private readonly List<BaseWeapon> _weapons = new();
    private readonly List<GameObject> _weaponsGO = new();
    private int _currentWeaponIndex;
    private BaseWeapon _currentWeapon;
    private WeaponSettings _currentSettings;

    public BaseWeapon CurrentWeapon => _currentWeapon;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        // Создаём оружие из настроек
        foreach (var ws in weaponSettingsList)
        {
            BaseWeapon weapon = ws.isContinuous
                ? new ContinuousWeapon(ws, playerCamera, inventory)
                : new SingleShotWeapon(ws, playerCamera, inventory);
            inventory.AddAmmo(ws.ammoType, 100);
            _weapons.Add(weapon);
        }

        if (_weapons.Count > 0)
        {
            _currentWeapon = _weapons[0];
            _currentSettings = _currentWeapon.Settings;
            WeaponChanged?.Invoke(_currentWeapon);
        }
    }
    private void Update()
    {
        if (_currentWeapon == null) return;

        bool isHeld = inputReader.IsFiringHeld();
        bool justPressed = inputReader.GetFire();
        bool aimHeld = inputReader.IsAimingHeld();

        _currentWeapon.UpdateWeapon(isHeld, justPressed);
        _currentWeapon.SetAiming(aimHeld);

        if (inputReader.GetReload())
            _currentWeapon.Reload();

        UpdateCameraAim(aimHeld);
        HandleWeaponSwitch();
        _currentWeapon.HandleReloadTimer();
    }

    private void UpdateCameraAim(bool isAiming)
    {
        if (_currentSettings == null || !_currentSettings.canAim)
            return;

        float targetFOV = isAiming ? _currentSettings.aimFOV : _currentSettings.normalFOV; 
        _cinemaCamera.Lens.FieldOfView = Mathf.Lerp(
            _cinemaCamera.Lens.FieldOfView,
            targetFOV,
            Time.deltaTime * _currentSettings.aimTransitionSpeed
        );
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
        _currentSettings = _weapons[index].Settings;

        WeaponChanged?.Invoke(_currentWeapon);
    }
}
