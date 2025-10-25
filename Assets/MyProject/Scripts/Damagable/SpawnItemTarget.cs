using System.Collections.Generic;
using UnityEngine;

public class SpawnItemTarget : Damagable
{
    [SerializeField] private List<Collectable> _itemPrefab;
    [SerializeField] private VFXEffectController _effect;

    private void Start()
    {
        _effect = GetComponentInChildren<VFXEffectController>();
        _effect.SetEffect(_destroySound);
    }
    protected override void Die()
    {
        if (_effect != null)
        {
            _effect.gameObject.transform.SetParent(null);
            _effect.PlayEffect();
        }

        if (_itemPrefab != null)
        {
            int rndIndex = Random.Range(0, _itemPrefab.Count);

            Collectable newCollectable = Instantiate(_itemPrefab[rndIndex], transform.position, Quaternion.identity);
            Rigidbody rb = newCollectable.gameObject.GetComponent<Rigidbody>();

            // Базовое направление вверх
            Vector3 direction = Vector3.up;

            // Добавляем случайное отклонение по X и Z
            direction.x += Random.Range(-0.5f, 0.5f);
            direction.z += Random.Range(-0.5f, 0.5f);

            // Нормализуем, чтобы сила всегда была одинаковой длины
            direction.Normalize();

            // Применяем силу
            float forceAmount = 5f;
            rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        }

        base.Die();
    }
}
