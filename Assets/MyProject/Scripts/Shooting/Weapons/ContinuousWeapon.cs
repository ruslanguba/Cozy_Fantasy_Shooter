using UnityEngine;

public class ContinuousWeapon : BaseWeapon
{
    private float _nextDamageTime;

    public ContinuousWeapon(WeaponSettings settings, Camera camera)
        : base(settings, camera) { }

    public override void UpdateWeapon(bool isFiringHeld)
    {
        if (!isFiringHeld) return;

        if (Time.time >= _nextDamageTime)
        {
            FireRay();
            _nextDamageTime = Time.time + _settings.continuousDamageInterval;
        }
    }
}
