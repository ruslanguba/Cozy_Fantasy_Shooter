using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private PlayerInteractorDetector detector;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventory;
    private IInterractable _currentInteractable;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        if(detector == null)
        {
            detector = GetComponentInChildren<PlayerInteractorDetector>();
        }
        detector.InteractableDetected += SetInteractable;
        detector.InteractableLost += ResetInteractable;
    }

    private void OnEnable()
    {
        if (detector == null)
        {
            detector = GetComponentInChildren<PlayerInteractorDetector>();
        }
        detector.InteractableDetected += SetInteractable;
        detector.InteractableLost += ResetInteractable;
    }

    private void OnDisable()
    {
        detector.InteractableDetected -= SetInteractable;
        detector.InteractableLost -= ResetInteractable;
    }

    private void Update()
    {
        if (inputReader.GetInterract())
            Interact();
    }

    private void Interact()
    {
        if (_currentInteractable == null) return;
            _currentInteractable.Interract(inventory);
        Debug.Log(_currentInteractable);
    }

    private void SetInteractable(IInterractable interractable)
    {
        Debug.Log("found");
        _currentInteractable = interractable;
    }

    private void ResetInteractable()
    {
        Debug.Log("Lost");
        _currentInteractable = null;
    }
}
