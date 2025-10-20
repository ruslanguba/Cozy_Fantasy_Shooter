using UnityEngine;

public class PlayerRotationCameraDepends : MonoBehaviour
{
    [SerializeField] private Transform _playerBody;
    private Camera _camera;
    private Transform _cameraTransform;

    private void Awake()
    {
        _camera = Camera.main;
        _cameraTransform = _camera.transform;
    }

    private void LateUpdate()
    {
        if (_playerBody == null || _cameraTransform == null) return;
        Vector3 camEuler = _cameraTransform.eulerAngles;
        _playerBody.rotation = Quaternion.Euler(0f, camEuler.y, 0f);
    }
}
