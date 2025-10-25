using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private TextMeshProUGUI totalAmmoText;
    [SerializeField] private TextMeshProUGUI magazineText;
    [SerializeField] private Image _currentWeaponIcon;
    [SerializeField] private Image _secondWeaponIcon;

    private BaseWeapon _currentWeapon;
    private bool _isWeaponIconActive = false;
    public void Initialize(WeaponManager weaponManager, Inventory inventory)
    {
        _weaponManager = weaponManager;
        _inventory = inventory;
        _inventory.OnAmmoChanged += UpdateTotalAmmo;
        _weaponManager.OnWeaponChanged += OnWeaponSwitched;
        _inventory.OnWeaponCollected += OnWeaponCollected;
        if (_weaponManager.CurrentWeapon != null)
            SubscribeToWeapon(_weaponManager.CurrentWeapon);
        if (_currentWeaponIcon.color.a != 0)
        {
            Color color = _currentWeaponIcon.color;
            color.a = 0;
            _currentWeaponIcon.color = color;
        }
    }

    private void OnDisable()
    {
        _inventory.OnAmmoChanged -= UpdateTotalAmmo;
        _inventory.OnWeaponCollected -= OnWeaponCollected;
        _weaponManager.OnWeaponChanged -= OnWeaponSwitched;
        UnsubscribeFromWeapon(_currentWeapon);
    }

    private void SubscribeToWeapon(BaseWeapon weapon)
    {
        if (weapon == null)
        {
            Debug.Log("No weapon");
            return;
        }
        _currentWeapon = weapon;
        _currentWeapon.AmmoInMagazineñChanged += UpdateMagazine;
        UpdateMagazine(_currentWeapon.GetCurrentAmmo);
        UpdateTotalAmmo(_currentWeapon.Settings.ammoType, _inventory.GetAmmo(_currentWeapon.Settings.ammoType));
        //UpdateIcon();
    }

    private void UnsubscribeFromWeapon(BaseWeapon weapon)
    {
        if (weapon == null)
        {
            Debug.Log("No weapon");
            return;
        }
        _currentWeapon.AmmoInMagazineñChanged -= UpdateMagazine;

    }

    private void UpdateMagazine(int current)
    {
        magazineText.text = current.ToString();
    }

    private void UpdateTotalAmmo(string itemId, int total)
    {
        if (_currentWeapon != null && itemId == _currentWeapon.Settings.ammoType)
        {
            totalAmmoText.text = total.ToString();
        }
    }

    public void OnWeaponSwitched(BaseWeapon newWeapon)
    {
        UnsubscribeFromWeapon(_currentWeapon);
        SubscribeToWeapon(newWeapon);
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if(_currentWeaponIcon.color.a == 0)
        {
            Color color = _currentWeaponIcon.color;
            color.a = 1;
            _currentWeaponIcon.color = color;

        }
        if (_isWeaponIconActive) SwitchIcon();
    }

    private void OnWeaponCollected(WeaponSettings newWeapon)
    {
        if (!_isWeaponIconActive)
        {   
            _currentWeapon = _weaponManager.CurrentWeapon;
            _currentWeaponIcon.sprite = _currentWeapon.Settings?.icon;
            _isWeaponIconActive = true;
        }
        else
        {
            _secondWeaponIcon.sprite = newWeapon.icon;
            if (_secondWeaponIcon.color.a == 0)
            {
                Color color = _currentWeaponIcon.color;
                color.a = 1;
                _secondWeaponIcon.color = color;
            }
        }
    }
    private void SwitchIcon()
    {
        Sprite sprite = _currentWeaponIcon.sprite;
        _currentWeaponIcon.sprite = _secondWeaponIcon.sprite;
        _secondWeaponIcon.sprite = sprite;
    }
}
