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

    private void OnEnable()
    {
        inputReader.OnFire += HandleSingleShotFire;
        inputReader.OnReload += HandleReload;
        inputReader.OnScroll += HandleWeaponSwitch;
    }

    private void OnDisable()
    {
        inputReader.OnFire -= HandleSingleShotFire;
        inputReader.OnReload -= HandleReload;
        inputReader.OnScroll -= HandleWeaponSwitch;
    }

    private void Update()
    {
        if (weaponManager.CurrentWeapon == null) return;

        // ContinuousWeapon проверяет удержание кнопки каждый кадр
        if (weaponManager.CurrentWeapon is ContinuousWeapon continuous)
        {
            bool isHeld = inputReader.IsFiringHeld();
            continuous.UpdateWeapon(isHeld, isHeld); // просто держим флаг
        }
        weaponManager.CurrentWeapon.HandleReloadTimer();
    }

    private void HandleSingleShotFire()
    {
        if (weaponManager.CurrentWeapon is SingleShotWeapon single)
        {
            single.UpdateWeapon(true, true); // одноразовый выстрел
            single.HandleReloadTimer();
        }
    }

    private void HandleReload()
    {
        weaponManager.CurrentWeapon?.Reload();
    }

    private void HandleWeaponSwitch(float scroll)
    {
        Debug.Log(scroll);
        if (weaponManager.CurrentWeapon == null) return;

        if (scroll > 0) weaponManager.NextWeapon();
        else if (scroll < 0) weaponManager.PrevWeapon();
    }
}
