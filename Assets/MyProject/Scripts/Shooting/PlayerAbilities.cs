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
    private void Update()
    {
        if (_inputReader.GetThrow())
            _launchHandler.Throw();
    }
}
