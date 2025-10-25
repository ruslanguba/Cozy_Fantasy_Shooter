using UnityEngine;

public class CameraLook : MonoBehaviour
{
    //[Header("References")]
    //[SerializeField] private Transform cameraFollowTarget; // pivot для камеры (LookAt/Follow)
    //[SerializeField] private Transform playerBody;         // объект игрока (для yaw)
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
    //    // Чтение инпута
    //    targetLookDelta = inputReader.GetLook() * sensitivity;

    //    // Плавное сглаживание
    //    currentLookDelta = Vector2.SmoothDamp(currentLookDelta, targetLookDelta, ref lookVelocity, smoothTime);

    //    yaw += currentLookDelta.x;
    //    pitch -= currentLookDelta.y;
    //    pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

    //    // Вращаем игрока по Y (горизонталь)
    //    playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);

    //    // Вращаем follow target по X (вертикаль)
    //    cameraFollowTarget.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    //}
}
