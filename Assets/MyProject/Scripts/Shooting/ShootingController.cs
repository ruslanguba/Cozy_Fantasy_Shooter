using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();
        inputReader = GetComponent<InputReader>();
    }
    private void Update()
    {
        if (weaponManager.CurrentWeapon == null) return;

        bool isHeld = inputReader.IsFiringHeld();
        bool justPressed = inputReader.GetFire();

        weaponManager.CurrentWeapon.UpdateWeapon(isHeld, justPressed);
        weaponManager.CurrentWeapon.HandleReloadTimer();
        if (inputReader.GetReload())
            weaponManager.CurrentWeapon.Reload();

        // Переключение оружия
        //float scroll = Input.mouseScrollDelta.y;
        float scroll = inputReader.GetScroll();
        if (Mathf.Abs(scroll) > 0.1f)
        {
            if (scroll > 0)
                weaponManager.NextWeapon();
            else
                weaponManager.PrevWeapon();
        }
    }
}
