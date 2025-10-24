using Unity.VisualScripting;
using UnityEngine;

public class UICollectPanelHendler : MonoBehaviour
{
    [SerializeField] private PlayerInteractorDetector _interactorDetector;
    [SerializeField] private GameObject _panel;

    public void Initialize(PlayerInteractorDetector interactorDetector)
    {
        _interactorDetector = interactorDetector;
        _interactorDetector.InteractableDetected += ShowPanel;
        _interactorDetector.InteractableLost += HidePanel;
        HidePanel();
    }

    private void OnDisable()
    {
        _interactorDetector.InteractableDetected -= ShowPanel;
        _interactorDetector.InteractableLost -= HidePanel;
    }

    private void ShowPanel(IInterractable i)
    {
        _panel.SetActive(true);
    }

    private void HidePanel()
    {
        _panel.SetActive(false);
    }
}
