using UnityEngine;

public class WeaponViewSetup : MonoBehaviour
{
    public bool IsInitialize { get; private set;}
    [SerializeField] private WeaponSettings settings;
    [SerializeField] private Transform muzzlePoint;
    private WeaponVFX _vfx;
    private BaseWeapon _weapon;

    public void Initialize(BaseWeapon weapon)
    {
        _vfx = new WeaponVFX(settings.muzzleFlashPrefab, settings.impactEffectPrefab, muzzlePoint, Camera.main);
        _weapon = weapon;
        _weapon.Shoot += OnShoot;
        _weapon.StopShooting += OnStopShooting;
        _weapon.Hit += OnHit;
        IsInitialize = true;
    }

    private void OnShoot()
    {
        _vfx.PlayMuzzleFlash(muzzlePoint);
    }

    private void OnStopShooting()
    {
        _vfx.StopMuzzleFlash();
        _vfx.StopImpact();
    }

    private void OnHit(Vector3 position, Vector3 normal)
    {
        _vfx.PlayImpact(position, normal);
    }

    private void OnDisable()
    {
        _weapon.Shoot -= OnShoot;
        _weapon.Hit -= OnHit;
        _weapon.StopShooting -= OnStopShooting;
    }
}
