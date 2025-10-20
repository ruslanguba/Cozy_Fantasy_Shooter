using UnityEngine;

public class SingleShotWeapon : BaseWeapon
{
    private bool _wasFiringLastFrame;
    private float _fireTimer;
    public SingleShotWeapon(WeaponSettings settings, Camera camera)
        : base(settings, camera) { }

    public override void UpdateWeapon(bool isFiringHeld)
    {
        // Стреляем ТОЛЬКО при нажатии (переход от false -> true)
        if (isFiringHeld && !_wasFiringLastFrame && _fireTimer <= 0)
        {
            FireRay();
            _fireTimer = _settings.fireRate;
        }
        if (_fireTimer >= 0 )
        {
            _fireTimer -= Time.deltaTime;
        }
        _wasFiringLastFrame = isFiringHeld;
    }
}
