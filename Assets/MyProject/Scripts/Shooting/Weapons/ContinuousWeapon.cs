using UnityEngine;

public class ContinuousWeapon : BaseWeapon
{
    public ContinuousWeapon(WeaponSettings settings, Camera camera, Inventory inventory)
        : base(settings, camera, inventory) { }

    public override void UpdateWeapon(bool isHeld, bool justPressed)
    {
        if (!isHeld)
        {
            StopShooting?.Invoke();
            return;
        }

        if (Time.time >= _nextFireTime)
        {
            TryShoot();
            _nextFireTime = Time.time + 1f / _settings.fireRate;
        }
    }
}
