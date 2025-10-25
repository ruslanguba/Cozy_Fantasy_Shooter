using System;
using Unity.Cinemachine;
using UnityEngine;

public class AIMManager : MonoBehaviour
{
    public Action<bool> IsAiming;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private CinemachineCamera _cinemaCamera;
    [SerializeField] private WeaponManager _weaponManager;
    private BaseWeapon _currentWeapon;
    private bool _lastAimState;

    private void Awake()
    {
        if(_weaponManager == null)
            _weaponManager = GetComponent<WeaponManager>();
        _currentWeapon = _weaponManager.CurrentWeapon;
        _weaponManager.OnWeaponChanged += OnWeaponChange;
    }

    private void OnDisable()
    {
        _weaponManager.OnWeaponChanged -= OnWeaponChange;
    }

    private void Update()
    {
        if (_currentWeapon == null) return;

        bool aimHeld = inputReader.IsAimingHeld();
        UpdateCameraAim(aimHeld);
        if (aimHeld != _lastAimState)
        {
            IsAiming?.Invoke(aimHeld);
            _lastAimState = aimHeld;
        }
    }

    private void UpdateCameraAim(bool isAiming)
    {
        if (_currentWeapon?.Settings == null || !_currentWeapon.Settings.canAim)
            return;

        float targetFOV = isAiming ? _currentWeapon.Settings.aimFOV : _currentWeapon.Settings.normalFOV;
        _cinemaCamera.Lens.FieldOfView = Mathf.Lerp(
            _cinemaCamera.Lens.FieldOfView,
            targetFOV,
            Time.deltaTime * _currentWeapon.Settings.aimTransitionSpeed
        );
    }

    private void OnWeaponChange(BaseWeapon weapon)
    {
        _currentWeapon = weapon;
    }
}
