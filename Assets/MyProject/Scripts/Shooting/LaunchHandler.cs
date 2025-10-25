using UnityEngine;

public class LaunchHandler : MonoBehaviour
{
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 10f;
    [SerializeField] private float upwardForce = 3f;

    private ThrowableManager _throwableManager;
    private float _reloadTimer;

    private void Awake()
    {
        _throwableManager = GetComponent<ThrowableManager>();
    }

    public void Throw()
    {
        ThrowableSettings current = _throwableManager.GetCurrentThrowable();
        if (current == null || current.prefab == null)
        {
            Debug.LogWarning("No throwable selected or prefab missing!");
            return;
        }

        if (CanThrow())
        {
            _reloadTimer = current.reloadTime;
            GameObject obj = Instantiate(current.prefab, throwPoint.position, Quaternion.identity);

            if (obj.TryGetComponent(out Rigidbody rb))
            {
                Vector3 direction = throwPoint.transform.forward;
                rb.AddForce(direction * throwForce + Vector3.up * upwardForce, ForceMode.Impulse);
            }

            if (obj.TryGetComponent(out IThrowable throwable))
            {
                throwable.Initialize(current);
            }
        }
    }

    private void Update()
    {
        Reaload();
    }

    private void Reaload()
    {
        if(_reloadTimer >= 0)
            _reloadTimer -= Time.deltaTime;
    }

    private bool CanThrow() => _reloadTimer <= 0;
}
