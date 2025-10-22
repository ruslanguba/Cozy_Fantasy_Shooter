using UnityEngine;

public class Collectable : MonoBehaviour, IInterractable
{
    [SerializeField] private string _itemID;
    [SerializeField] private int _ammount;

    public void Interract(Inventory inventory)
    {
        Collect(inventory);
    }

    protected virtual void Collect(Inventory inventory)
    {
        inventory.AddAmmo(_itemID, _ammount);
        Debug.Log($"collected: {_itemID} , ammount: {_ammount}");
        Destroy( this );
    }
}
