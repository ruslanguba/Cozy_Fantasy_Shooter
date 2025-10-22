using UnityEngine;

public class SingleShotWeapon : BaseWeapon
{
    public SingleShotWeapon(WeaponSettings settings, Camera camera, Inventory inventory)
        : base(settings, camera, inventory) { }

    public override void UpdateWeapon(bool isHeld, bool justPressed)
    {
        if (!justPressed) return;

        if (Time.time >= _nextFireTime)
        {
            TryShoot();
            _nextFireTime = Time.time + 1f / _settings.fireRate;
        }
    }
}
