using Core.Game.Interface;
using Feature.TextureManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Game.Model;

public class Hearth : IPickable
{
    private readonly ITextureManager _textureManager = new TextureManager();
    private readonly ContentManager _contentManager;
    
    public bool IsPickedUp { get; set; }
    
    private Dictionary<int, Texture2D> _hearthTextures = new();
    public Vector2 Position;

    public Hearth(Vector2 position, IServiceProvider serviceProvider)
    {
        _contentManager = new ContentManager(serviceProvider, "Content");
        _hearthTextures = LoadHearthTexture();
        Position = position;
    }
    
    private Dictionary<int, Texture2D> LoadHearthTexture()
    {
        return _textureManager.LoadHearthTextures(_contentManager);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (IsPickedUp) return;
        if (_hearthTextures.Count == 0)
        {
            Console.WriteLine("Hearth textures is null. Cannot draw hearth.");
            return;
        }
        
        _hearthTextures.TryGetValue(12, out var hearthTexture);
        
        spriteBatch.Draw(
            hearthTexture,
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