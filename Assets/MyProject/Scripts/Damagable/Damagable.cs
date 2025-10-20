using UnityEngine;

public class Damagable : MonoBehaviour, IDamageable
{
    public void TakeDamage(float damage)
    {
        Debug.Log(damage);
    }
}
