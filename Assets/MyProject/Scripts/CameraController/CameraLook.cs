using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //[Header("References")]
    //[SerializeField] private Transform cameraFollowTarget; // pivot ��� ������ (LookAt/Follow)
    //[SerializeField] private Transform playerBody;         // ������ ������ (��� yaw)
    //[SerializeField] private InputReader inputReader;

    //[Header("Settings")]
    //[SerializeField] private float sensitivity = 1.0f;
    //[SerializeField] private float minPitch = -80f;
    //[SerializeField] private float maxPitch = 80f;
    //[SerializeField] private float smoothTime = 0.05f;

    //private Vector2 currentLookDelta;
    //private Vector2 targetLookDelta;
    //private Vector2 lookVelocity;

    //private float pitch; // X rotation
    //private float yaw;   // Y rotation

    //private void Awake()
    //{
    //    inputReader = GetComponent<InputReader>();
    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    //private void LateUpdate()
    //{
    //    RotateCharacter();
    //}

    //private void RotateCharacter()
    //{
    //    // ������ ������
    //    targetLookDelta = inputReader.GetLook() * sensitivity;

    //    // ������� �����������
    //    currentLookDelta = Vector2.SmoothDamp(currentLookDelta, targetLookDelta, ref lookVelocity, smoothTime);

    //    yaw += currentLookDelta.x;
    //    pitch -= currentLookDelta.y;
    //    pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

    //    // ������� ������ �� Y (�����������)
    //    playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);

    //    // ������� follow target �� X (���������)
    //    cameraFollowTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    //}
}
