using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Scriptable Objects/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapon Info")]
    public GameObject weaponPrefab;
    public string weaponName = "Rifle";
    public string ammoType = "Rifle";
    public int magazineSize = 30;
    public float fireRate = 0.1f;
    public float reloadTime = 1.5f;
    public float damage = 20f;
    public float range = 100f;
    public bool isContinuous;

    [Header("Aim Settings")]
    public bool canAim = true;
    public float normalFOV = 60;
    public float aimFOV = 40f;
    public float aimSensitivityMultiplier = 0.5f;
    public float aimTransitionSpeed = 8f;

    [Header("Visual Effects")]
    public GameObject muzzleFlashPrefab;
    public GameObject impactEffectPrefab;
    public Transform muzzlePivot;
}
