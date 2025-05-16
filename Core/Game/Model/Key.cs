using Core.Game.Interface;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Model;

public class Key : IPickable
{
    private readonly ITextureManager _textureManager = new TextureManager();
    private readonly ContentManager _contentManager;
    public bool IsPickedUp { get; set; }
    private Dictionary<int, Texture2D> _keyTexture = new();
    public Vector2 Position;

    public Key(Vector2 position, IServiceProvider serviceProvider)
    {
        _contentManager = new ContentManager(serviceProvider, "Content");
        _keyTexture = LoadKeyTexture();
        Position = position;
    }
    
    private Dictionary<int, Texture2D> LoadKeyTexture()
    {
        return _textureManager.LoadKeyTextures(_contentManager);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsPickedUp) return;
        if (_keyTexture.Count == 0)
        {
            Console.WriteLine("Key textures is null. Cannot draw key.");
            return;
        }
        
        _keyTexture.TryGetValue(11, out var keyTexture);
        
        spriteBatch.Draw(
            keyTexture,
            Position,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            1f,
            SpriteEffects.None,
            0.0f);
    }

    public void PickUp()
    {
        IsPickedUp = true;
    }
}