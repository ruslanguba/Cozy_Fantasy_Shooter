using UnityEngine;

public class TransformTarget : Damagable
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private VFXEffectController _effect;
    private void Start()
    {
        _effect = GetComponentInChildren<VFXEffectController>();
    }
    protected override void Die()
    {
        _effect.gameObject.transform.SetParent(null);
        _effect.PlayEffect();

        if (itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }

        base.Die();
    }
}
