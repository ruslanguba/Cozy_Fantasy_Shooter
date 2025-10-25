using UnityEngine;

public class TransformTarget : Damagable
{
    [SerializeField] private GameObject _newObjectPrefab;
    [SerializeField] private VFXEffectController _effect;
    [SerializeField] private Transform _destroyEffectPivot;

    protected override void Die()
    {
        if (_newObjectPrefab != null)
        {
            Instantiate(_newObjectPrefab, transform.position, Quaternion.identity);
        }
        if (_effect != null)
        {
            VFXEffectController effect = Instantiate(_effect, transform.position, Quaternion.identity);
            effect.SetEffect(_destroySound);
            effect.PlayEffect();
        }

        base.Die();
    }
}
