using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public event Action<BaseWeapon> OnWeaponChanged;

    [SerializeField] private Transform weaponPivot;
    private Camera _camera;
    private Inventory _inventory;

    private List<BaseWeapon> _weapons = new List<BaseWeapon>();
    private List<WeaponViewSetup> _weaponViews = new List<WeaponViewSetup>();
    private int _currentIndex = -1;

    public BaseWeapon CurrentWeapon => _currentIndex >= 0 ? _weapons[_currentIndex] : null;

    private void OnEnable()
    {
        _inventory = GetComponent<Inventory>();
        _camera = Camera.main;
        _inventory.OnWeaponCollected += CollectWeapon;
    }

    private void OnDisable()
    {
        _inventory.OnWeaponCollected -= CollectWeapon;
    }

    public void CollectWeapon(WeaponSettings settings)
    {
        if (_weapons.Exists(w => w.Settings.weaponName == settings.weaponName))
            return;

        BaseWeapon weapon = settings.isContinuous
            ? new ContinuousWeapon(settings, _camera, _inventory)
            : new SingleShotWeapon(settings, _camera, _inventory);

        _weapons.Add(weapon);
        _inventory.AddAmmo(settings.ammoType, 100);

        if (settings.weaponPrefab != null)
        {
            GameObject viewObj = Instantiate(settings.weaponPrefab, weaponPivot);
            WeaponViewSetup view = viewObj.GetComponent<WeaponViewSetup>();
            view.Initialize(settings);
            viewObj.SetActive(false);

            _weaponViews.Add(view);
        }

        if (_weapons.Count == 1)
            SwitchWeapon(0);
    }

    public void SwitchWeapon(int newIndex)
    {
        if (newIndex < 0 || newIndex >= _weapons.Count)
            return;

        if (_currentIndex >= 0)
        {
            _weaponViews[_currentIndex].UnbindWeapon();
            _weaponViews[_currentIndex].gameObject.SetActive(false);
        }

        _currentIndex = newIndex;

        _weaponViews[_currentIndex].gameObject.SetActive(true);
        _weaponViews[_currentIndex].BindWeapon(_weapons[_currentIndex]);
        OnWeaponChanged?.Invoke(_weapons[_currentIndex]);
    }

    public void NextWeapon() => SwitchWeapon((_currentIndex + 1) % _weapons.Count);
    public void PrevWeapon() => SwitchWeapon((_currentIndex - 1 + _weapons.Count) % _weapons.Count);
}
