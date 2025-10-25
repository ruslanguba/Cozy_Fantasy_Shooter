using UnityEngine;

public class SpawnItemTarget : Damagable
{
    [SerializeField] private GameObject itemPrefab;
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

        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        base.Die();
    }
}
