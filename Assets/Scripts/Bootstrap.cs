using UnityEngine;
using UnityEngine.InputSystem;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private InputActionAsset  _inputActions;
    [SerializeField] private MovementSettings _movementSettings;

    private InputService _inputService;
    
    private void Awake()
    {
       _inputService = new InputService(_inputActions); 
       _player.Initialize(_groundCheckPos, _inputService, _movementSettings);
    }
    
    private void OnDestroy()
    {
        _inputService.Dispose();
    }
}
