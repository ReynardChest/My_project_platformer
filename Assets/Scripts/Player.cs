using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] private PlayerMovement _playerMovement;
    
    private InputService _inputService;

    public void Initialize(Transform groundCheckPos, InputService inputService, MovementSettings movementSettings)
    {
        _playerMovement = new PlayerMovement(GetComponent<Rigidbody2D>(), groundCheckPos, movementSettings);
        _inputService = inputService;
        
        _inputService.Moved += _playerMovement.Move;
        _inputService.Jumped += _playerMovement.Jump;
        
    }

    private void Update()
    {
        _playerMovement.OnUpdate();
    }

    public void TakeDamage()
    {
        SceneManager.LoadScene(0);
    }
}
