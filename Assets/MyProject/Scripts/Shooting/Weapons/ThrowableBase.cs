using UnityEngine;

public abstract class ThrowableBase : MonoBehaviour, IThrowable
{
    public abstract void Initialize(ThrowableSettings settings);
}
