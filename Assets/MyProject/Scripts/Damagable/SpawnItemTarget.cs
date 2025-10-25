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

            // ������� ����������� �����
            Vector3 direction = Vector3.up;

            // ��������� ��������� ���������� �� X � Z
            direction.x += Random.Range(-0.5f, 0.5f);
            direction.z += Random.Range(-0.5f, 0.5f);

            // �����������, ����� ���� ������ ���� ���������� �����
            direction.Normalize();

            // ��������� ����
            float forceAmount = 5f;
            rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        }

        base.Die();
    }
}
