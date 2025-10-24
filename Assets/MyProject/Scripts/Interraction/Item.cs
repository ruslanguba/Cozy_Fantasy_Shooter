using UnityEngine;

public class Item : Collectable
{
    [SerializeField] private WeaponSettings weaponSettings;
    protected override void Collect(Inventory inventory)
    {
        base.Collect(inventory);
        inventory.AddWeapon(weaponSettings);
    }
}
