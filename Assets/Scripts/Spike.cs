using DefaultNamespace;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage();
        }
    }
}
