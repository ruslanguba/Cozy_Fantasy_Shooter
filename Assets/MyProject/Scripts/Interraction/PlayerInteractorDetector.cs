using System;
using UnityEngine;

public class PlayerInteractorDetector : MonoBehaviour
{
    public Action<IInterractable> InteractableDetected;
    public Action InteractableLost;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _interactionDistance = 3f;
    [SerializeField] private LayerMask _interactionMask;

    private IInterractable _currentInteractable;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        DetectInteractable();
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactionMask))
        {
            if (hit.collider.TryGetComponent<IInterractable>(out var interactable))
            {
                if (_currentInteractable != interactable)
                {
                    _currentInteractable = interactable;
                    InteractableDetected?.Invoke(interactable);
                }
                return;
            }
        }

        // Если луч больше не смотрит на объект
        if (_currentInteractable != null)
        {
            _currentInteractable = null;
            InteractableLost?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        if (_camera == null) return;

        Gizmos.color = _currentInteractable != null ? Color.green : Color.red;
        Vector3 start = _camera.transform.position;
        Vector3 end = start + _camera.transform.forward * _interactionDistance;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }
}
