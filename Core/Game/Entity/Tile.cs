using System.Numerics;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Entity
{
    public class Tile
    {
        private Texture2D _texture;
        private TileCollision _collision;

        private const int Width = 32;
        private const int Height = 32;
        
        public static readonly Vector2 Size = new Vector2(Width, Height);

        public Tile(Texture2D texture, TileCollision collision)
        {
            _texture = texture;
            _collision = collision;
        }
    }
}
