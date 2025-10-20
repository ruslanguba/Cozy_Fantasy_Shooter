using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerContext : MonoBehaviour
{
    [SerializeField] private PlayerSettings _settings;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _cameraPivot;

    private CharacterController _controller;
    private PlayerMovement _movement;
    private PlayerGravity _gravity;
    private PlayerJump _jump;
    private PlayerLook _look;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();

        _movement = new PlayerMovement(_settings, _controller, _cameraPivot, _inputReader);
        _gravity = new PlayerGravity(_settings, _controller);
        _jump = new PlayerJump(_settings, _controller, _gravity, _inputReader);
        _look = new PlayerLook(_settings, _cameraPivot, transform, _inputReader);
    }

    private void Update()
    {
        _look.UpdateLook();
        _movement.UpdateMovement();
        _gravity.UpdateGravity();
        _jump.HandleJump();
        _movement.ApplyMovement(_gravity.VerticalVelocity);
    }
}
