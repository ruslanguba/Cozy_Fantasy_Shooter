using UnityEngine;

[CreateAssetMenu(fileName = "ThrowableSettings", menuName = "Scriptable Objects/ThrowableSettings")]
public class ThrowableSettings : ScriptableObject
{
    public GameObject prefab;
    public string throwableName;
    public float reloadTime = 3f;
    public float damageRadius = 5f;
    public float damage;
    public bool stickToSurfaces;
    public Sprite icon;

    public VFXEffectController effect;
    public AudioClip destroySound;
}
