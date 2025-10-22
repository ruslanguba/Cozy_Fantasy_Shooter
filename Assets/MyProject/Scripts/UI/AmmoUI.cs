using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI totalAmmoText;
    [SerializeField] private TextMeshProUGUI magazineText;

    private BaseWeapon _currentWeapon;

    private void OnEnable()
    {
        inventory.OnAmmoChanged += UpdateTotalAmmo;
        weaponManager.WeaponChanged += SubscribeToWeapon;
    }

    private void OnDisable()
    {
        inventory.OnAmmoChanged -= UpdateTotalAmmo;
        weaponManager.WeaponChanged -= SubscribeToWeapon;
        UnsubscribeFromWeapon(_currentWeapon);
    }

    private void Start()
    {
        if (weaponManager.CurrentWeapon != null)
            SubscribeToWeapon(weaponManager.CurrentWeapon);
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
        UpdateTotalAmmo(_currentWeapon.Settings.ammoType, inventory.GetAmmo(_currentWeapon.Settings.ammoType));
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
        totalAmmoText.text = total.ToString();
    }

    // Âûçûâàòü ïðè ñìåíå îðóæèÿ
    public void OnWeaponSwitched(BaseWeapon newWeapon)
    {
        UnsubscribeFromWeapon(_currentWeapon);
        SubscribeToWeapon(newWeapon);
    }
}
