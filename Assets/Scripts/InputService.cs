using System;
using UnityEngine.InputSystem;

public class InputService : IDisposable 
{    
    private const string Player = nameof(Player);
    private const string Move = nameof(Move);
    private const string Jump = nameof(Jump);
    
    private readonly InputAction _move;
    private readonly InputAction _jump;
    private readonly InputActionAsset _actions;
    
    public InputService(InputActionAsset actions)
    {
        _actions = actions;
        InputActionMap map = _actions.FindActionMap(Player);
        _move = map.FindAction(Move);
        _jump = map.FindAction(Jump);
        
        _move.performed += ctx => Moved?.Invoke(ctx);
        _move.canceled  += ctx => Moved?.Invoke(ctx);
        _jump.performed += ctx => Jumped?.Invoke(ctx);
        _jump.canceled  += ctx => Jumped?.Invoke(ctx);
        
        _actions.Enable();
    }

    public event Action<InputAction.CallbackContext> Moved;
    public event Action<InputAction.CallbackContext> Jumped;
    
    public void Dispose()
    {
        _move.performed -= ctx => Moved?.Invoke(ctx);
        _move.canceled  -= ctx => Moved?.Invoke(ctx);
        _jump.performed -= ctx => Jumped?.Invoke(ctx);
        _jump.canceled  -= ctx => Jumped?.Invoke(ctx);
        
        _actions.Disable();
    }
}
