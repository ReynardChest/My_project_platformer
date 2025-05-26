using UnityEngine;
using System;

public class TrapTrigger : MonoBehaviour
{
    public event Action PlayerStepped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            PlayerStepped?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
