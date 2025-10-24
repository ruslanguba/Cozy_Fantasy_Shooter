using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Action<BaseWeapon> WeaponChanged;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private List<GameObject> _weaponView = new();

    private WeaponHolder _holder;
    private BaseWeapon _currentWeapon;
    private WeaponViewSetup _currentView;

    public BaseWeapon CurrentWeapon => _currentWeapon;

    private void Awake()
    {
        if (!playerCamera) playerCamera = Camera.main;
        _holder = new WeaponHolder(playerCamera, inventory);
        _holder.BindToInventory();
        _holder.OnWeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _holder.UnbindFromInventory();
        _holder.OnWeaponChanged -= OnWeaponChanged;
    }

    private void Update()
    {
        if (_currentWeapon == null) return;

        bool isHeld = inputReader.IsFiringHeld();
        bool justPressed = inputReader.GetFire();

        _currentWeapon.UpdateWeapon(isHeld, justPressed);

        if (inputReader.GetReload())
            _currentWeapon.Reload();

        if (Input.mouseScrollDelta.y > 0) _holder.NextWeapon();
        else if (Input.mouseScrollDelta.y < 0) _holder.PrevWeapon();

        _currentWeapon.HandleReloadTimer();
    }

    private void OnWeaponChanged(BaseWeapon weapon)
    {
        _currentWeapon = weapon;

        if (_currentView != null)
            Destroy(_currentView.gameObject);

        if (weapon.Settings.weaponPrefab != null)
        {
            GameObject viewObj = Instantiate(weapon.Settings.weaponPrefab, weaponPivot.position, weaponPivot.rotation, weaponPivot);
            _currentView = viewObj.GetComponent<WeaponViewSetup>();
            _currentView.Initialize(_currentWeapon);
        }
        WeaponChanged?.Invoke(_currentWeapon);
    }
}
