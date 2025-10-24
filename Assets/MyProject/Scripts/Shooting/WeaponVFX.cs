using System.Collections.Generic;
using UnityEngine;

public class WeaponVFX
{
    private readonly List<ParticleSystem> _muzzleParticles = new();
    private readonly List<ParticleSystem> _impactParticles = new();
    private readonly Camera _camera;
    private readonly float _maxDistance = 100f;

    public WeaponVFX(GameObject muzzlePrefab, GameObject impactPrefab, Transform parent, Camera camera)
    {
        _camera = camera;

        if (muzzlePrefab != null)
        {
            GameObject muzzleObj = Object.Instantiate(muzzlePrefab, parent);
            _muzzleParticles.AddRange(muzzleObj.GetComponentsInChildren<ParticleSystem>());
        }

        if (impactPrefab != null)
        {
            GameObject impactObj = Object.Instantiate(impactPrefab);
            _impactParticles.AddRange(impactObj.GetComponentsInChildren<ParticleSystem>());
        }
    }

    public void PlayMuzzleFlash(Transform spawnPoint)
    {
        if (_muzzleParticles.Count == 0 || _camera == null)
            return;

        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(_maxDistance);

        Vector3 lookDir = (targetPoint - spawnPoint.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(lookDir);

        foreach (var ps in _muzzleParticles)
        {
            ps.transform.SetPositionAndRotation(spawnPoint.position, lookRotation);
            ps.Play();
        }
    }
    public void StopMuzzleFlash()
    {
        foreach (var ps in _muzzleParticles)
        {
            ps.Stop();
        }
    }

    public void PlayImpact(Vector3 position, Vector3 normal)
    {
        foreach (var ps in _impactParticles)
        {
            ps.transform.SetPositionAndRotation(position, Quaternion.LookRotation(normal));
            ps.Play();
        }
    }

    public void StopImpact()
    {
        foreach (var ps in _impactParticles)
        {
            ps.Stop();
        }
    }
}
