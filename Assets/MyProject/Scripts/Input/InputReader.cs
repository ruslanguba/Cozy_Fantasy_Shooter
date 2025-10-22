using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _jumpAction;

    [SerializeField] private InputActionReference _fireAction;
    [SerializeField] private InputActionReference _aimAction;
    [SerializeField] private InputActionReference _reloadAction;

    [SerializeField] private InputActionReference _throwAction;

    [SerializeField] private InputActionReference _interractAction;

    private void OnEnable()
    {
        if (_moveAction?.action != null) _moveAction.action.Enable();
        if (_lookAction?.action != null) _lookAction.action.Enable();
        if (_jumpAction?.action != null) _jumpAction.action.Enable();
        if (_fireAction?.action != null) _fireAction.action.Enable();
        if (_aimAction?.action != null) _aimAction.action.Enable();
        if (_reloadAction?.action != null) _reloadAction.action.Enable();
        if (_throwAction?.action != null) _throwAction.action.Enable();
        if (_interractAction?.action != null) _interractAction.action.Enable();
    }

    private void OnDisable()
    {
        if (_moveAction?.action != null) _moveAction.action.Disable();
        if (_lookAction?.action != null) _lookAction.action.Disable();
        if (_jumpAction?.action != null) _jumpAction.action.Disable();
        if (_fireAction?.action != null) _fireAction.action.Disable();
        if (_aimAction?.action != null) _aimAction.action.Disable();
        if (_reloadAction?.action != null) _reloadAction.action.Disable();
        if (_throwAction?.action != null) _throwAction.action.Disable();
        if (_interractAction?.action != null) _interractAction.action.Disable();
    }
    public Vector2 GetMove() => _moveAction?.action != null ? _moveAction.action.ReadValue<Vector2>() : Vector2.zero;

    public Vector2 GetLook() => _lookAction?.action != null ? _lookAction.action.ReadValue<Vector2>() : Vector2.zero;

    public bool GetJump() => _jumpAction?.action != null ? _jumpAction.action.triggered : false;

    public bool GetFire() => _fireAction?.action != null && _fireAction.action.triggered;
    public bool IsFiringHeld() => _fireAction?.action != null && _fireAction.action.IsPressed();
    public bool IsAimingHeld() => _aimAction?.action != null && _aimAction.action.IsPressed();

    public bool GetReload() => _reloadAction?.action != null && _reloadAction.action.triggered;
    public bool GetThrow() => _throwAction?.action != null && _throwAction.action.triggered;
    public bool GetInterract() => _interractAction?.action != null && _interractAction.action.triggered;

}
