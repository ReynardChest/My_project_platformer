using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] public float MoveSpeed;

    [Header("Jump")]
    [SerializeField] public float JumpForce;
    [SerializeField] public int MaxJumps;

    [Header("Gravity")]
    [SerializeField] public float BaseGravity;
    [SerializeField] public float FallSpeedMultiplier;
    [SerializeField] public float MaxFallSpeed;
}
