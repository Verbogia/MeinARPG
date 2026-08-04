public enum PlayerCommandType
{
    None,
    Move,
    Attack,
    Interact
}

public struct PlayerCommand
{
    public PlayerCommandType Type;
    public UnityEngine.Vector3 WorldPoint;
    public UnityEngine.Transform Target;
    public bool IsHeld;
}
