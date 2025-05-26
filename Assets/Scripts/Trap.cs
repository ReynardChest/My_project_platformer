using UnityEngine;

public class Trap : MonoBehaviour
{
    private const float SpikesYMove = 1.25f;
    
    [SerializeField] private TrapTrigger _trigger;
    [SerializeField] private Transform _spikes;

    private void Awake()
    {
        _trigger.PlayerStepped += OnMoveSpikes;
    }

    private void OnDestroy()
    {
        _trigger.PlayerStepped -= OnMoveSpikes;
    }

    private void OnMoveSpikes()
    {
        _spikes.transform.position = new Vector2(_spikes.transform.position.x, _spikes.transform.position.y - SpikesYMove);
    }
}
