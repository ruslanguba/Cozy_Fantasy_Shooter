using UnityEngine;

public abstract class InterractableBase : MonoBehaviour, IInterractable
{
    public abstract void Interract(Inventory inventory);
}
