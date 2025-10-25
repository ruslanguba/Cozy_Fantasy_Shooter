using UnityEngine;

public class WeaponViewSetup : MonoBehaviour
{
    [SerializeField] private Transform muzzlePoint;
    private WeaponVFX _vfx;
    private BaseWeapon _weapon;
    private WeaponSettings _settings;
    [SerializeField] private AudioSource _impactAudioSource;

    public void Initialize(WeaponSettings settings)
    {
        _settings = settings;
        _vfx = new WeaponVFX(settings.muzzleFlashPrefab, settings.impactEffectPrefab, muzzlePoint, Camera.main);

    }
    public void BindWeapon(BaseWeapon weapon)
    {
        _weapon = weapon;
        _weapon.Shoot += OnShoot;
        _weapon.Hit += OnHit;
        _weapon.StopShooting += OnStopShooting;
    }

    public void UnbindWeapon()
    {
        _weapon.Shoot -= OnShoot;
        _weapon.Hit -= OnHit;
        _weapon.StopShooting -= OnStopShooting;
        _weapon = null;
    }

    public void OnShoot()
    {
        _vfx.PlayMuzzleFlash(muzzlePoint);
        if (_settings.muzzleSound == null)
            return;

        if (_weapon.Settings.isContinuous)
        {
            if (!AudioManager.Instance.sfxSource.isPlaying)
            {
                AudioManager.Instance.sfxSource.clip = _settings.muzzleSound;
                AudioManager.Instance.sfxSource.loop = true;
                AudioManager.Instance.sfxSource.Play();
            }
        }
        else
        {
            // ƒл€ одиночных Ч просто короткий звук
            AudioManager.Instance.sfxSource.PlayOneShot(_settings.muzzleSound);
        }
    }

    public void OnStopShooting()
    {
        if (_weapon.Settings.isContinuous && AudioManager.Instance.sfxSource.isPlaying)
        {
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.sfxSource.loop = false;
            _impactAudioSource.Stop();
        }
        _vfx.StopMuzzleFlash();
        _vfx.StopImpact();
    }

    public void OnHit(Vector3 position, Vector3 normal, bool hitSomething)
    {
        if (_settings.impactSound == null) return;

        if (_weapon.Settings.isContinuous)
        {
            if (hitSomething)
            {
                if (!_impactAudioSource.isPlaying)
                {
                    _impactAudioSource.clip = _settings.impactSound;
                    _impactAudioSource.loop = true;
                    _impactAudioSource.Play();
                }
                _vfx.PlayImpact(position, normal);
            }
            else
            {
                if (_impactAudioSource.isPlaying)
                {
                    _impactAudioSource.loop = false;
                    _impactAudioSource.Stop();
                }
                _vfx.StopImpact();
            }

        }
        else if (hitSomething)
        {
            _impactAudioSource.PlayOneShot(_settings.impactSound);
            _vfx.PlayImpact(position, normal);
        }
    }

}
