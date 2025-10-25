using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VFXEffectController : MonoBehaviour
{
    private readonly List<ParticleSystem> _particles = new();
    [SerializeField] private AudioClip _destroySound;
    [SerializeField] private AudioSource _source;

    public void SetEffect(AudioClip destroySound)
    {
        _destroySound = destroySound;
    }

    void Awake()
    {
        if(gameObject.TryGetComponent<AudioSource>(out AudioSource source))
        _source = source;
        _particles.AddRange(gameObject.GetComponentsInChildren<ParticleSystem>());
    }

    public void PlayEffect()
    {
        foreach (var ps in _particles)
        {
            ps.Play();
        }
        if (_destroySound != null)
        {
            _source.PlayOneShot(_destroySound);
        }
        Destroy(gameObject, 5);
    }
}
