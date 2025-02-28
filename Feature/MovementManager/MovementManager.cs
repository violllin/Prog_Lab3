using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Feature.MovementManager;

public class MovementManager : IMovementManager
{
    public (float, float) HandleMovement()
    {
        float x = 0;
        float y = 0;

        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
        {
            y -= 1;
        }

        if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
        {
            y += 1;
        }

        if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
        {
            x -= 1;
        }

        if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
        {
            x += 1;
        }

        return (x, y);
    }

    private bool IsTileObstacle(int tileKey)
    {
        return tileKey == 1 || tileKey == 2;
    }
    

    public bool CheckCollision(Rectangle playerRect, TileMap tileMap)
    {
        var tileSize = GameDefaults.TileSize;
            
        int startX = Math.Max(0, playerRect.Left / tileSize);
        int startY = Math.Max(0, playerRect.Top / tileSize);
        int endX = Math.Min(tileMap.Width - 1, playerRect.Right / tileSize);
        int endY = Math.Min(tileMap.Height - 1, playerRect.Bottom / tileSize);

        for (int y = startY; y <= endY; y++)
        {
            for (int x = startX; x <= endX; x++)
            {
                var tileKey = tileMap.Tiles[y][x];

                if (IsTileObstacle(tileKey)) continue;
                var tileRect = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                if (playerRect.Intersects(tileRect))
                {
                    return true;
                }
            }
        }

        return false;
    }
}