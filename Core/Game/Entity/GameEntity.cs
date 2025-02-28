using System.Numerics;

namespace Core.Game.Entity;

public class GameEntity
{
    public Vector2 Position { get; set; }
    public string Id;
    public string Type;
    public bool IsActive;
}