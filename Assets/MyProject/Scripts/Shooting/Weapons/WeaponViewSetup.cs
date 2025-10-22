using UnityEngine;

public class WeaponViewSetup : MonoBehaviour
{
    [SerializeField] private WeaponSettings settings;
    [SerializeField] private Transform muzzlePoint;


    private void Awake()
    {
        settings.muzzlePivot = muzzlePoint;
    }
}
