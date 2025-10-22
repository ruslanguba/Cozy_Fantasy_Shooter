using UnityEngine;

public class InterractionHandler : MonoBehaviour
{
    [SerializeField] private PlayerInterractorDetector detector;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventory;
    private IInterractable _currentInteractable;

    private void Awake()
    {
        inputReader = GetComponent<InputReader>();
        if(detector == null)
        {
            detector = GetComponentInChildren<PlayerInterractorDetector>();
        }
        detector.InterractableDetected += SetInterractable;
        detector.InterractableLost += ResetInterractable;
    }

    private void OnEnable()
    {
        if (detector == null)
        {
            detector = GetComponentInChildren<PlayerInterractorDetector>();
        }
        detector.InterractableDetected += SetInterractable;
        detector.InterractableLost += ResetInterractable;
    }

    private void OnDisable()
    {
        detector.InterractableDetected -= SetInterractable;
        detector.InterractableLost -= ResetInterractable;
    }

    private void Update()
    {
        if (inputReader.GetInterract())
            Interract();
    }

    private void Interract()
    {
        _currentInteractable.Interract(inventory);
    }

    private void SetInterractable(IInterractable interractable)
    {
        Debug.Log("found");
        _currentInteractable = interractable;
    }

    private void ResetInterractable()
    {
        _currentInteractable = null;
    }
}
