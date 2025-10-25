using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private LaunchHandler _launchHandler;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _launchHandler = GetComponent<LaunchHandler>();
    }
    private void OnEnable()
    {
        _inputReader.OnThrow += _launchHandler.Throw;
    }
    private void OnDisable()
    {
        _inputReader.OnThrow -= _launchHandler.Throw;
    }
}
