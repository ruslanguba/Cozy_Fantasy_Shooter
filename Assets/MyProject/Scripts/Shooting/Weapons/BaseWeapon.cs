using UnityEngine;

public abstract class BaseWeapon
{
    protected readonly WeaponSettings _settings;
    protected readonly Camera _camera;

    protected BaseWeapon(WeaponSettings settings, Camera camera)
    {
        _settings = settings;
        _camera = camera;
    }

    public abstract void UpdateWeapon(bool isFiringHeld);
    protected void FireRay()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, _settings.range))
        {
            if (hit.collider.TryGetComponent(out IDamageable target))
                target.TakeDamage(_settings.damage);
        }
        else
        {
            Debug.Log($"[{_settings.weaponName}] Fired into void");
        }
    }

    public virtual void Reload()
    {
        Debug.Log($"{_settings.weaponName} reloading...");
    }
}
