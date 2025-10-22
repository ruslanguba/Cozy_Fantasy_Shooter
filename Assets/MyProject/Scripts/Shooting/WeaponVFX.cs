using UnityEngine;

public class WeaponVFX
{
    private ParticleSystem _muzzleFlash;
    private ParticleSystem _impactEffect;

    public WeaponVFX(GameObject muzzlePrefab, GameObject impactPrefab, Transform parent)
    {
        if (muzzlePrefab != null)
        {
            GameObject muzzleObj = Object.Instantiate(muzzlePrefab, parent);
            _muzzleFlash = muzzleObj.GetComponent<ParticleSystem>();
        }

        if (impactPrefab != null)
        {
            GameObject impactObj = Object.Instantiate(impactPrefab);
            _impactEffect = impactObj.GetComponent<ParticleSystem>();
        }
    }

    public void PlayMuzzleFlash(Transform spawnPoint)
    {
        if (_muzzleFlash == null) return;

        _muzzleFlash.transform.position = spawnPoint.position;
        _muzzleFlash.transform.rotation = spawnPoint.rotation;
        _muzzleFlash.Play();
    }

    public void PlayImpact(Vector3 position, Vector3 normal)
    {
        if (_impactEffect == null) return;

        _impactEffect.transform.position = position;
        _impactEffect.transform.rotation = Quaternion.LookRotation(normal);
        _impactEffect.Play();
    }
}
