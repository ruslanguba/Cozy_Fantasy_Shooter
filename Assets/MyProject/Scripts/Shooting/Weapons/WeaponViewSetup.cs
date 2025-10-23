using UnityEngine;

public class WeaponViewSetup : MonoBehaviour
{
    [SerializeField] private WeaponSettings settings;
    [SerializeField] private Transform muzzlePoint;
    protected WeaponVFX _vfx;

    private void Awake()
    {
        settings.muzzlePivot = muzzlePoint;
    }
}
