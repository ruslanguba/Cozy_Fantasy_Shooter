
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    [SerializeField] Damagable _targetPrefab;
    [SerializeField] private Transform[] _movingRows; // 3 объекта
    [SerializeField] private float _moveDistance = 2.2f;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private List<Transform> _platforms;
    private readonly List<Damagable> _spawnedTargets = new List<Damagable>();
    [SerializeField] private Damagable _targets;
    [SerializeField] Activator _activator;
    private int _spawnedTargetsCount;
    private int _destroyedTargetsCount;

    private bool[] _movingRight;
    private bool _isActive;

    private void OnEnable()
    {
        _activator.OnActivate += Act;
    }

    private void OnDisable()
    {
        _activator.OnActivate -= Act;
        ClearOldTargets();
    }

    private void Start()
    {
        _movingRight = new bool[_movingRows.Length];
        RandomizeDirections();
    }

    private void Update()
    {
        if (!_isActive) return;

        for (int i = 0; i < _movingRows.Length; i++)
        {
            if (_movingRows[i] == null) continue;

            float dir = _movingRight[i] ? 1 : -1;
            _movingRows[i].Translate(Vector3.right * dir * _moveSpeed * Time.deltaTime);

            if (_movingRows[i].localPosition.x >= _moveDistance)
                _movingRight[i] = false;
            else if (_movingRows[i].localPosition.x <= -_moveDistance)
                _movingRight[i] = true;
        }
    }

    public void Act()
    {
        _isActive = !_isActive;
        SpawnTargets();
        if (_isActive)
            RandomizeDirections(); // при активации новое направление
    }

    private void RandomizeDirections()
    {
        if (_movingRight == null) _movingRight = new bool[_movingRows.Length];

        for (int i = 0; i < _movingRows.Length; i++)
            _movingRight[i] = Random.value > 0.5f;
    }

    private void SpawnTargets()
    {
        for (int i = 0; i < _platforms.Count; i++)
        {   
            Vector3 spownPosition = new Vector3(_platforms[i].transform.position.x, _platforms[i].transform.position.y+ 0.5f, _platforms[i].transform.position.z);
            Transform lookTarget = _activator.transform;
            Vector2 direction = lookTarget.position - spownPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Damagable newTarget = Instantiate(_targetPrefab, spownPosition, Quaternion.identity, _platforms[i]);
            newTarget.OnDestroy += CountDestroy;
            _spawnedTargets.Add(newTarget);
            _spawnedTargetsCount++;
        }
    }

    private void ClearOldTargets()
    {
        foreach (var target in _spawnedTargets)
        {
            if (target != null)
                target.OnDestroy -= CountDestroy;
            Destroy(target.gameObject);
        }

        _spawnedTargets.Clear();
        _destroyedTargetsCount = 0;
    }

    private void CountDestroy()
    {
        _destroyedTargetsCount++;
        if (_destroyedTargetsCount >= _spawnedTargetsCount) 
        {
            Compleat();
        }
    }

    private void Compleat()
    {
        Debug.Log("OK");
        ClearOldTargets();
    }
}
