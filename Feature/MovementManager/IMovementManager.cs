using Microsoft.Xna.Framework;

namespace Feature.MovementManager;

public interface IMovementManager
{
    (float, float) HandleMovement();
    
    bool CheckCollision(Rectangle playerRect, TileMap tileMap);
}