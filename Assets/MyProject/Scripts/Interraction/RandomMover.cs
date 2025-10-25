using UnityEngine;

public class RandomMover : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _height = 3f;
    [SerializeField] private float _minY = 0f;

    private Vector3 _startPos;
    private float _direction;
    private float _timeOffset;

    private void Start()
    {
        _startPos = transform.position;
        _timeOffset = Random.Range(0f, 1f);
        _direction = Random.value > 0.5f ? 1f : -1f;
    }

    private void Update()
    {
        float yOffset = Mathf.PingPong((Time.time + _timeOffset) * _speed, _height) * _direction;
        float y = Mathf.Max(_startPos.y + yOffset, _minY);
        transform.position = new Vector3(_startPos.x, y, _startPos.z);
    }
}
