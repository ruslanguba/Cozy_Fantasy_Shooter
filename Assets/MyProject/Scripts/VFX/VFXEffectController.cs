using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VFXEffectController : MonoBehaviour
{
    private readonly List<ParticleSystem> _particles = new();
    void Start()
    {
        _particles.AddRange(gameObject.GetComponentsInChildren<ParticleSystem>());
    }

    public void PlayEffect()
    {
        foreach (var ps in _particles)
        {
            ps.Play();
            Destroy(gameObject, 2);
        }
    }
}
