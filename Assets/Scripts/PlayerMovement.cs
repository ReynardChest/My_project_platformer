using System;
using UnityEngine;
using UnityEngine.InputSystem;


//https://github.com/DawnosaurDev/platformer-movement/blob/main/Platformer%20Demo%20-%20Unity%20Project/Assets/Scripts/PlayerMovement.cs


[Serializable] 
public class PlayerMovement 
{
    private readonly Rigidbody2D _rb;
    private readonly MovementSettings _movementSettings;
    
    [Header("Movement")]
    private float _horizontalMovement;

    [Header("Jump")] 
    [SerializeField] private int _jumpRemaining; 
    private bool _isJumpCut;
    private bool _isJumpFalling;
    
    [Header("GroundCheck")]
    private Transform _groundCheckPos;
    [SerializeField] private Vector2 _groungCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] private LayerMask _groundLayer = LayerMask.GetMask("Ground");

    public PlayerMovement(Rigidbody2D rb, Transform groundCheckPos, MovementSettings movementSettings)
    {
        _rb = rb;
        _groundCheckPos = groundCheckPos;
        _movementSettings = movementSettings;
    }

    // Состояния
    [field: SerializeField] public bool IsJumping { get; private set; }

    public void OnUpdate()
    {
        _rb.linearVelocity = new Vector2(_horizontalMovement * _movementSettings.MoveSpeed, _rb.linearVelocity.y);
        GroundCheck();
        UpdateFallingGravity();
    }

    public void Move(InputAction.CallbackContext context) =>
        _horizontalMovement = context.ReadValue<Vector2>().x;
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (_jumpRemaining <= 0) 
            return;
        
        if (context.performed)
        {
            float force = _movementSettings.JumpForce;
                
            if (_rb.linearVelocity.y < 0)
                force -= _rb.linearVelocity.y * 2;
                
            _rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            _jumpRemaining--;
        }
        else if (context.canceled)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y * 0.5f);
            _jumpRemaining--;
        }
    }

    private void UpdateFallingGravity()
    {
        if (_rb.linearVelocity.y < 0)
        {
            _rb.gravityScale = _movementSettings.FallSpeedMultiplier * _movementSettings.BaseGravity;
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, Mathf.Max(_rb.linearVelocity.y,-_movementSettings.MaxFallSpeed));
        }
    }  
    
    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(_groundCheckPos.position, _groungCheckSize, 0, _groundLayer))
        {
            _jumpRemaining = _movementSettings.MaxJumps;
        }
    } 
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(_groundCheckPos.position, _groungCheckSize);
    }
}

