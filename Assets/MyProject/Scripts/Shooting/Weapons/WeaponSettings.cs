using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Scriptable Objects/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName = "Rifle";
    public int magazineSize = 30;
    public float fireRate = 0.1f;
    public float reloadTime = 1.5f;
    public float damage = 20f;
    public float range = 100f;
    public float continuousDamageInterval = 0.2f;
    public bool isContinuous;

    [Header("References")]
    public Transform firePoint;
}
