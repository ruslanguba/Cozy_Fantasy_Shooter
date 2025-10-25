using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        continueButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
        restartButton.onClick.AddListener(() => GameManager.Instance.RestartGame());
        exitButton.onClick.AddListener(() => GameManager.Instance.ExitToMainMenu());
    }
}
