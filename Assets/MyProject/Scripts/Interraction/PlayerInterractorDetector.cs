using System;
using UnityEngine;

public class PlayerInterractorDetector : MonoBehaviour
{
    public Action<IInterractable> InterractableDetected;
    public Action InterractableLost;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IInterractable>(out IInterractable component))
        {
            InterractiveObjectDetected(component);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IInterractable>(out IInterractable component))
        {
            InterractiveObjectLost();
        }
    }

    private void InterractiveObjectDetected(IInterractable interractable)
    {
        InterractableDetected?.Invoke(interractable);
    }
    private void InterractiveObjectLost()
    {
        InterractableLost?.Invoke();
    }
}
